using System.ComponentModel;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Internal;

/// <summary>
/// Low-level connection to the AMPECO Public API: applies authentication,
/// builds requests, maps errors to <see cref="AmpecoApiException"/>, and unwraps envelopes.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ApiConnection : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly AmpecoClientOptions _options;
    private readonly bool _ownsHttpClient;
    private readonly Uri _baseAddress;

    /// <summary>Creates a connection from the provided options.</summary>
    public ApiConnection(AmpecoClientOptions options)
    {
        _options = options;
        _ownsHttpClient = options.HttpClient is null;

        if (_ownsHttpClient)
        {
            _baseAddress = options.GetBaseAddress();
            _httpClient = new HttpClient { BaseAddress = _baseAddress, Timeout = Timeout.InfiniteTimeSpan };
        }
        else
        {
            // A caller-supplied HttpClient (including the one IHttpClientFactory hands to the
            // typed client) is never mutated here: BaseAddress and Timeout cannot be assigned
            // once it has issued a request, so writing to them can throw. Its own BaseAddress
            // wins when set; the per-request timeout is enforced with a linked token instead.
            _httpClient = options.HttpClient!;
            _baseAddress = _httpClient.BaseAddress ?? options.GetBaseAddress();
        }
    }

    /// <summary>Default items-per-page taken from the client options.</summary>
    public int DefaultPerPage => _options.DefaultPerPage;

    /// <inheritdoc />
    public void Dispose()
    {
        if (_ownsHttpClient)
        {
            _httpClient.Dispose();
        }
    }

    // ---- single-item helpers -------------------------------------------------

    /// <summary>Sends a GET request and deserializes the <c>data</c> envelope.</summary>
    public async Task<T> GetAsync<T>(string path, QueryBuilder? query = null, CancellationToken cancellationToken = default)
    {
        var json = await SendAsync(HttpMethod.Get, path, query, requestBody: null, expectBody: true, cancellationToken).ConfigureAwait(false);
        return DeserializeEnvelope<T>(json, path);
    }

    /// <summary>Sends a POST request and deserializes the <c>data</c> envelope.</summary>
    public Task<T> PostAsync<T>(string path, object? requestBody = null, QueryBuilder? query = null, CancellationToken cancellationToken = default)
        => SendWithBodyAsync<T>(HttpMethod.Post, path, query, requestBody, cancellationToken);

    /// <summary>Sends a PUT request and deserializes the <c>data</c> envelope.</summary>
    public Task<T> PutAsync<T>(string path, object? requestBody = null, QueryBuilder? query = null, CancellationToken cancellationToken = default)
        => SendWithBodyAsync<T>(HttpMethod.Put, path, query, requestBody, cancellationToken);

    /// <summary>Sends a PATCH request and deserializes the <c>data</c> envelope.</summary>
    public Task<T> PatchAsync<T>(string path, object? requestBody = null, QueryBuilder? query = null, CancellationToken cancellationToken = default)
        => SendWithBodyAsync<T>(HttpMethod.Patch, path, query, requestBody, cancellationToken);

    /// <summary>Sends a DELETE request and deserializes the <c>data</c> envelope.</summary>
    public Task<T> DeleteAsync<T>(string path, object? requestBody = null, QueryBuilder? query = null, CancellationToken cancellationToken = default)
        => SendWithBodyAsync<T>(HttpMethod.Delete, path, query, requestBody, cancellationToken);

    /// <summary>Sends a POST request and deserializes a raw (non-enveloped) JSON body, e.g. action responses.</summary>
    public async Task<T> PostUnwrappedAsync<T>(string path, object? requestBody = null, CancellationToken cancellationToken = default)
    {
        var json = await SendAsync(HttpMethod.Post, path, query: null, requestBody, expectBody: true, cancellationToken).ConfigureAwait(false);
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new AmpecoProtocolException($"Expected a JSON body from '{path}' but the response was empty.");
        }

        return JsonSerializer.Deserialize<T>(json, Json.Serializer)
               ?? throw new AmpecoProtocolException($"Response from '{path}' could not be parsed.");
    }

    /// <summary>Sends a request where a non-empty success body is not guaranteed (202 Accepted, 204 No Content).</summary>
    public async Task SendAsyncNoContent(HttpMethod method, string path, object? requestBody = null, QueryBuilder? query = null, CancellationToken cancellationToken = default)
    {
        await SendAsync(method, path, query, requestBody, expectBody: false, cancellationToken).ConfigureAwait(false);
    }

    private async Task<T> SendWithBodyAsync<T>(HttpMethod method, string path, QueryBuilder? query, object? requestBody, CancellationToken cancellationToken)
    {
        var json = await SendAsync(method, path, query, requestBody, expectBody: true, cancellationToken).ConfigureAwait(false);
        return DeserializeEnvelope<T>(json, path);
    }

    // ---- listing helpers -----------------------------------------------------

    /// <summary>Sends a listing GET request and returns a <see cref="Page{T}"/>.</summary>
    public async Task<Page<T>> GetPageAsync<T>(string path, QueryBuilder? query, CancellationToken cancellationToken = default)
    {
        var json = await SendAsync(HttpMethod.Get, path, query, requestBody: null, expectBody: true, cancellationToken).ConfigureAwait(false);
        var envelope = JsonSerializer.Deserialize<ApiListEnvelope<T>>(json, Json.Serializer)
                       ?? throw new AmpecoProtocolException($"Listing response for '{path}' could not be parsed.");
        var (next, prev, currentPage, total, lastPage, perPage) = PageMetaReader.Read(envelope.Meta);
        return new Page<T>
        {
            Data = envelope.Data,
            NextCursor = next ?? (envelope.Links?.Next is { } nextUrl ? ReadCursorFromUrl(nextUrl) : null),
            PrevCursor = prev ?? (envelope.Links?.Prev is { } prevUrl ? ReadCursorFromUrl(prevUrl) : null),
            CurrentPage = currentPage,
            Total = total,
            LastPage = lastPage,
            PerPage = perPage,
        };
    }

    /// <summary>Iterates all items of a listing endpoint using cursor pagination.
    /// Sends an empty <c>cursor</c> on the first request to opt into cursor pagination.
    /// </summary>
    public async IAsyncEnumerable<T> StreamAsync<T>(
        string path,
        QueryBuilder? query,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        query ??= new QueryBuilder();
        string? cursor = null;
        int? nextPageNumber = null;

        while (true)
        {
            query.Set("cursor", cursor ?? string.Empty);
            if (nextPageNumber is { } pageNumber)
            {
                query.Set("page", pageNumber.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }

            var page = await GetPageAsync<T>(path, query, cancellationToken).ConfigureAwait(false);

            foreach (var item in page.Data)
            {
                yield return item;
            }

            if (page.NextCursor is { Length: > 0 } next)
            {
                cursor = next;
                nextPageNumber = null;
                continue;
            }

            // Legacy page-based endpoint: advance one page at a time from the page we
            // just read. Jumping to LastPage + 1 here would skip every page in between.
            if (page.CurrentPage is { } current && page.LastPage is { } last && current < last)
            {
                cursor = null;
                nextPageNumber = current + 1;
                continue;
            }

            yield break;
        }
    }

    private static string? ReadCursorFromUrl(string url)
    {
        var idx = url.IndexOf("cursor=", StringComparison.Ordinal);
        if (idx < 0)
        {
            return null;
        }

        var value = url[(idx + "cursor=".Length)..];
        var amp = value.IndexOf('&');
        if (amp >= 0)
        {
            value = value[..amp];
        }

        return Uri.UnescapeDataString(value);
    }

    // ---- core send -----------------------------------------------------------

    private async Task<string> SendAsync(HttpMethod method, string path, QueryBuilder? query, object? requestBody, bool expectBody, CancellationToken cancellationToken)
    {
        var url = path;
        if (query is { IsEmpty: false })
        {
            url += "?" + query;
        }

        using var request = new HttpRequestMessage(method, new Uri(_baseAddress, url));

        if (!string.IsNullOrEmpty(_options.ApiKey))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiKey);
        }

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (requestBody is not null)
        {
            request.Content = new StringContent(
                JsonSerializer.Serialize(requestBody, requestBody.GetType(), Json.Serializer),
                Encoding.UTF8,
                "application/json");
        }

        using var timeout = CreateTimeoutSource(cancellationToken);
        var token = timeout?.Token ?? cancellationToken;

        using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);

        var body = await response.Content.ReadAsStringAsync(token).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            throw CreateException(response.StatusCode, body);
        }

        if (!expectBody && (string.IsNullOrWhiteSpace(body) || body.Trim() == "[]"))
        {
            return string.Empty;
        }

        return body;
    }

    private CancellationTokenSource? CreateTimeoutSource(CancellationToken cancellationToken)
    {
        if (_options.RequestTimeout == Timeout.InfiniteTimeSpan || _options.RequestTimeout <= TimeSpan.Zero)
        {
            return null;
        }

        var source = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        source.CancelAfter(_options.RequestTimeout);
        return source;
    }

    private static T DeserializeEnvelope<T>(string json, string path)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new AmpecoProtocolException($"Expected a JSON body from '{path}' but the response was empty.");
        }

        try
        {
            var envelope = JsonSerializer.Deserialize<ApiEnvelope<T>>(json, Json.Serializer);
            if (envelope is null || envelope.Data is null)
            {
                throw new AmpecoProtocolException($"Response from '{path}' did not contain a 'data' property.");
            }

            return envelope.Data;
        }
        catch (JsonException ex)
        {
            throw new AmpecoProtocolException($"Response from '{path}' could not be parsed: {ex.Message}", ex);
        }
    }

    private static AmpecoApiException CreateException(HttpStatusCode statusCode, string body)
    {
        string? message = null;
        Dictionary<string, IReadOnlyList<string>>? errors = null;

        try
        {
            using var doc = JsonDocument.Parse(body);
            var root = doc.RootElement;
            if (root.ValueKind == JsonValueKind.Object)
            {
                if (root.TryGetProperty("message", out var msg) && msg.ValueKind == JsonValueKind.String)
                {
                    message = msg.GetString();
                }

                if (root.TryGetProperty("errors", out var errs) && errs.ValueKind == JsonValueKind.Object)
                {
                    errors = [];
                    foreach (var field in errs.EnumerateObject())
                    {
                        var values = field.Value.ValueKind == JsonValueKind.Array
                            ? field.Value.EnumerateArray().Where(e => e.ValueKind == JsonValueKind.String).Select(e => e.GetString() ?? string.Empty).ToArray()
                            : [field.Value.ToString()];
                        errors[field.Name] = values;
                    }
                }
            }
        }
        catch (JsonException)
        {
            // Not JSON - fall through with raw body.
        }

        return new AmpecoApiException((int)statusCode, message ?? body, body, errors);
    }
}
