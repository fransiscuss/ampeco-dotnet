using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Internal;

/// <summary>
/// Builds query strings for API requests, including AMPECO's deepObject-style
/// <c>filter[name]=value</c> parameters.
/// </summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class QueryBuilder
{
    private readonly List<(string Key, string? Value)> _params = [];

    /// <summary>Adds <c>key=value</c> when <paramref name="value"/> is not null.</summary>
    public void Add(string key, string? value)
    {
        if (value is not null)
        {
            _params.Add((key, value));
        }
    }

    /// <summary>Adds <c>key=value</c> when <paramref name="value"/> is not null.</summary>
    public void Add(string key, int? value) => Add(key, value?.ToString(System.Globalization.CultureInfo.InvariantCulture));

    /// <summary>Adds <c>key=value</c> when <paramref name="value"/> is not null.</summary>
    public void Add(string key, long? value) => Add(key, value?.ToString(System.Globalization.CultureInfo.InvariantCulture));

    /// <summary>Adds <c>key=value</c> when <paramref name="value"/> is not null.</summary>
    public void Add(string key, bool? value) => Add(key, value?.ToString().ToLowerInvariant());

    /// <summary>Adds <c>key=value</c> when <paramref name="value"/> is not null.</summary>
    public void Add(string key, DateTimeOffset? value) =>
        Add(key, value?.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", System.Globalization.CultureInfo.InvariantCulture));

    /// <summary>Adds <c>key=value</c> when <paramref name="value"/> is not null.</summary>
    public void Add(string key, DateOnly? value) =>
        Add(key, value?.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));

    /// <summary>
    /// Serializes a filter object as AMPECO deepObject parameters, e.g. <c>filter[userId]=123</c>.
    /// Arrays are emitted Laravel-style as repeated <c>filter[key][]</c> entries.
    /// </summary>
    public void AddFilter<TFilter>(TFilter? filter) where TFilter : class
    {
        if (filter is null)
        {
            return;
        }

        foreach (var entry in FilterSerializer.Enumerate(filter))
        {
            _params.Add(entry);
        }
    }

    /// <summary>Adds repeated <c>include[]=name</c> parameters.</summary>
    public void AddIncludes(IEnumerable<string>? includes)
    {
        foreach (var include in includes ?? [])
        {
            _params.Add(("include[]", include));
        }
    }

    /// <summary>Adds the paging parameters (cursor + per_page).</summary>
    public void AddPaging(PageRequest? request, int defaultPerPage)
    {
        // An empty "cursor" parameter opts the request into cursor pagination.
        _params.Add(("cursor", request?.Cursor ?? string.Empty));
        var perPage = request?.PerPage ?? defaultPerPage;
        _params.Add(("per_page", Math.Clamp(perPage, 1, 100).ToString(System.Globalization.CultureInfo.InvariantCulture)));
    }

    /// <summary>Replaces the value of the first matching key (or appends when absent).</summary>
    public void Set(string key, string? value)
    {
        for (var i = 0; i < _params.Count; i++)
        {
            if (_params[i].Key == key)
            {
                _params[i] = (key, value);
                return;
            }
        }

        _params.Add((key, value));
    }

    /// <summary>True when no parameters have been added.</summary>
    public bool IsEmpty => _params.Count == 0;

    /// <inheritdoc />
    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var (key, value) in _params)
        {
            if (sb.Length > 0)
            {
                sb.Append('&');
            }

            sb.Append(Uri.EscapeDataString(key));
            if (value is not null)
            {
                sb.Append('=').Append(Uri.EscapeDataString(value));
            }
        }

        return sb.ToString();
    }
}

/// <summary>
/// Enumerates a filter object's set properties as deepObject key/value pairs.
/// Uses the <see cref="JsonPropertyNameAttribute"/>-declared API property names.
/// </summary>
internal static class FilterSerializer
{
    private static readonly System.Collections.Concurrent.ConcurrentDictionary<Type, PropertyInfo[]> Cache = new();

    public static IEnumerable<(string Key, string Value)> Enumerate<TFilter>(TFilter filter) where TFilter : class
    {
        var props = Cache.GetOrAdd(filter.GetType(), static t => [.. t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.GetCustomAttribute<JsonIgnoreAttribute>() is null)]);

        foreach (var prop in props)
        {
            var value = prop.GetValue(filter);
            if (value is null)
            {
                continue;
            }

            var name = prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? prop.Name;

            if (value is System.Collections.IEnumerable enumerable and not string)
            {
                foreach (var item in enumerable)
                {
                    if (item is null)
                    {
                        continue;
                    }

                    yield return ($"filter[{name}][]", FormatScalar(item));
                }
            }
            else
            {
                yield return ($"filter[{name}]", FormatScalar(value));
            }
        }
    }

    private static string FormatScalar(object value) => value switch
    {
        bool b => b ? "true" : "false",
        DateTimeOffset dt => dt.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", System.Globalization.CultureInfo.InvariantCulture),
        DateOnly d => d.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
        IFormattable f => f.ToString(null, System.Globalization.CultureInfo.InvariantCulture),
        _ => value.ToString() ?? string.Empty,
    };
}
