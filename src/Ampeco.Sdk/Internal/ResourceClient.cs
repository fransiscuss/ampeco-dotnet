namespace Ampeco.Sdk.Internal;

/// <summary>
/// Shared plumbing for the typed resource sub-clients: path building,
/// paging defaults and envelope unwrapping.
/// </summary>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public abstract class ResourceClient
{
    /// <summary>The underlying API connection.</summary>
    protected ApiConnection Connection { get; }

    /// <summary>Creates a resource client over the given connection.</summary>
    protected ResourceClient(ApiConnection connection)
    {
        Connection = connection;
    }

    /// <summary>Performs a GET and returns the unwrapped <c>data</c> payload.</summary>
    protected async Task<T> GetAsync<T>(string path, QueryBuilder? query = null, CancellationToken cancellationToken = default)
        => await Connection.GetAsync<T>(path, query, cancellationToken).ConfigureAwait(false);

    /// <summary>Performs a POST and returns the unwrapped <c>data</c> payload.</summary>
    protected async Task<T> PostAsync<T>(string path, object? body = null, CancellationToken cancellationToken = default)
        => await Connection.PostAsync<T>(path, body, query: null, cancellationToken).ConfigureAwait(false);

    /// <summary>Performs a request that does not return a body.</summary>
    protected Task SendNoContentAsync(HttpMethod method, string path, object? body = null, CancellationToken cancellationToken = default)
        => Connection.SendAsyncNoContent(method, path, body, query: null, cancellationToken);

    /// <summary>Performs a DELETE.</summary>
    protected Task DeleteAsync(string path, CancellationToken cancellationToken = default)
        => Connection.SendAsyncNoContent(HttpMethod.Delete, path, requestBody: null, query: null, cancellationToken);

    /// <summary>Fetches a single page, applying paging defaults.</summary>
    protected Task<Page<T>> GetPageAsync<T>(string path, QueryBuilder? query, PageRequest? pageRequest, CancellationToken cancellationToken)
    {
        query ??= new QueryBuilder();
        query.AddPaging(pageRequest, Connection.DefaultPerPage);
        return Connection.GetPageAsync<T>(path, query, cancellationToken);
    }

    /// <summary>Streams all pages, applying paging defaults.</summary>
    protected IAsyncEnumerable<T> StreamAsync<T>(string path, QueryBuilder? query, PageRequest? pageRequest, CancellationToken cancellationToken)
    {
        query ??= new QueryBuilder();
        query.AddPaging(pageRequest, Connection.DefaultPerPage);
        return Connection.StreamAsync<T>(path, query, cancellationToken);
    }
}
