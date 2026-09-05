namespace Ampeco.Sdk;

/// <summary>
/// A single page of results from a listing endpoint.
/// Returned by the <c>GetPageAsync</c> methods; use <see cref="NextCursor"/>
/// to fetch subsequent pages, or the <c>StreamAsync</c> methods to iterate over everything.
/// </summary>
/// <typeparam name="T">The item type.</typeparam>
public sealed class Page<T>
{
    /// <summary>The items in this page.</summary>
    public required IReadOnlyList<T> Data { get; init; }

    /// <summary>Cursor to pass to the next request to fetch the following page, or null when no more pages exist.</summary>
    public string? NextCursor { get; init; }

    /// <summary>Cursor to fetch the previous page, when supported.</summary>
    public string? PrevCursor { get; init; }

    /// <summary>Number of items requested per page.</summary>
    public int? PerPage { get; init; }

    /// <summary>Page number when the endpoint uses legacy page-based pagination.</summary>
    public int? CurrentPage { get; init; }

    /// <summary>Total number of items across all pages (legacy page-based pagination only).</summary>
    public long? Total { get; init; }

    /// <summary>Total number of pages (legacy page-based pagination only).</summary>
    public int? LastPage { get; init; }

    /// <summary>True when there is a subsequent page.</summary>
    public bool HasNextPage => !string.IsNullOrEmpty(NextCursor) ||
                               (CurrentPage.HasValue && LastPage.HasValue && CurrentPage < LastPage);
}

/// <summary>
/// Paging controls for listing endpoints.
/// </summary>
public sealed class PageRequest
{
    /// <summary>Number of items to return per page (1–100). Defaults to <see cref="AmpecoClientOptions.DefaultPerPage"/>.</summary>
    public int? PerPage { get; init; }

    /// <summary>
    /// Opaque cursor from a previous page. Leave null to start from the beginning;
    /// pass <see cref="Page{T}.NextCursor"/> for subsequent pages.
    /// </summary>
    public string? Cursor { get; init; }

    public static implicit operator PageRequest(int perPage) => new() { PerPage = perPage };

    public static PageRequest First(int? perPage = null) => new() { PerPage = perPage };
}
