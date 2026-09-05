using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Internal;

/// <summary>Envelope for single-item responses: <c>{ "data": ... }</c>.</summary>
internal sealed class ApiEnvelope<T>
{
    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}

/// <summary>Envelope for listing responses: <c>{ "data": [...], "links": ..., "meta": ... }</c>.</summary>
internal sealed class ApiListEnvelope<T>
{
    [JsonPropertyName("data")]
    public List<T> Data { get; set; } = [];

    [JsonPropertyName("links")]
    public PageLinks? Links { get; set; }

    [JsonPropertyName("meta")]
    public JsonElement? Meta { get; set; }
}

internal sealed class PageLinks
{
    [JsonPropertyName("first")]
    public string? First { get; set; }

    [JsonPropertyName("last")]
    public string? Last { get; set; }

    [JsonPropertyName("prev")]
    public string? Prev { get; set; }

    [JsonPropertyName("next")]
    public string? Next { get; set; }
}

/// <summary>
/// Extracts the paging cursors from a listing response's <c>meta</c> object.
/// Handles both cursor-based (<c>next_cursor</c>/<c>prev_cursor</c>) and
/// legacy page-based (<c>current_page</c>/<c>last_page</c>/<c>total</c>) metadata.
/// </summary>
internal static class PageMetaReader
{
    public static (string? NextCursor, string? PrevCursor, int? CurrentPage, long? Total, int? LastPage, int? PerPage)
        Read(JsonElement? meta)
    {
        if (meta is not { } value || value.ValueKind != JsonValueKind.Object)
        {
            return (null, null, null, null, null, null);
        }

        var element = value;
        string? next = TryGetString(element, "next_cursor");
        string? prev = TryGetString(element, "prev_cursor");
        int? currentPage = TryGetInt(element, "current_page");
        long? total = TryGetLong(element, "total");
        int? lastPage = TryGetInt(element, "last_page");
        int? perPage = TryGetInt(element, "per_page");

        // The API also exposes the raw next cursor through meta.cursor.
        next ??= TryGetString(element, "cursor");

        return (next, prev, currentPage, total, lastPage, perPage);
    }

    private static string? TryGetString(JsonElement obj, string name) =>
        obj.TryGetProperty(name, out var v) && v.ValueKind == JsonValueKind.String ? v.GetString() : null;

    private static int? TryGetInt(JsonElement obj, string name) =>
        obj.TryGetProperty(name, out var v) && v.ValueKind == JsonValueKind.Number && v.TryGetInt32(out var i) ? i : null;

    private static long? TryGetLong(JsonElement obj, string name) =>
        obj.TryGetProperty(name, out var v) && v.ValueKind == JsonValueKind.Number && v.TryGetInt64(out var l) ? l : null;
}
