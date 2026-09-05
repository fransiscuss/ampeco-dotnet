using System.Net;
using System.Text;
using Ampeco.Sdk.Internal;

namespace Ampeco.Sdk.Tests;

/// <summary>
/// Test double that records requests and returns queued responses.
/// </summary>
public sealed class FakeHttpHandler : HttpMessageHandler
{
    private readonly Queue<(HttpStatusCode Status, string Body)> _responses = [];
    public List<HttpRequestMessage> Requests { get; } = [];
    public List<string?> RequestBodies { get; } = [];

    public void Enqueue(HttpStatusCode status, string body) => _responses.Enqueue((status, body));

    public void EnqueueJson(HttpStatusCode status, object payload) =>
        _responses.Enqueue((status, System.Text.Json.JsonSerializer.Serialize(payload)));

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Requests.Add(request);
        if (request.Content is not null)
        {
            RequestBodies.Add(await request.Content.ReadAsStringAsync(cancellationToken));
        }
        else
        {
            RequestBodies.Add(null);
        }

        if (_responses.Count == 0)
        {
            throw new InvalidOperationException("No queued response for " + request.RequestUri);
        }

        var (status, body) = _responses.Dequeue();
        return new HttpResponseMessage(status)
        {
            Content = new StringContent(body, Encoding.UTF8, "application/json"),
        };
    }
}

/// <summary>Shared helpers for creating configured clients.</summary>
public static class TestClient
{
    public static (AmpecoClient Client, FakeHttpHandler Handler) Create(Action<AmpecoClientOptions>? configure = null)
    {
        var handler = new FakeHttpHandler();
        var options = new AmpecoClientOptions
        {
            TenantUrl = "https://mytenant.ampeco.com",
            ApiKey = "test-token",
            HttpClient = new HttpClient(handler) { BaseAddress = new Uri("https://mytenant.ampeco.com/public-api/") },
            DefaultPerPage = 2,
        };
        configure?.Invoke(options);
        return (new AmpecoClient(options), handler);
    }
}
