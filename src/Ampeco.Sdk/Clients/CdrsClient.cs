using Ampeco.Sdk.Internal;
using Ampeco.Sdk.Models;

namespace Ampeco.Sdk;

/// <summary>
/// Read-only access to roaming Charge Data Records (cdrs v2.0).
/// </summary>
public sealed class CdrsClient : ResourceClient
{
    internal CdrsClient(ApiConnection connection) : base(connection)
    {
    }

    private const string ResourceBase = "resources/cdrs/v2.0";

    /// <summary>Fetches a CDR by id.</summary>
    public Task<Cdr> GetAsync(long cdrId, CancellationToken cancellationToken = default)
        => GetAsync<Cdr>($"{ResourceBase}/{cdrId}", cancellationToken: cancellationToken);

    /// <summary>Fetches a single page of CDRs.</summary>
    /// <param name="filter">Optional filters (serialized as deepObject query parameters).</param>
    /// <param name="pageRequest">Paging options; defaults to cursor pagination from the first page.</param>
    public Task<Page<Cdr>> GetPageAsync(CdrFilter? filter = null, PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return GetPageAsync<Cdr>(ResourceBase, query, pageRequest, cancellationToken);
    }

    /// <summary>Iterates over all CDRs matching the filter, transparently following cursor pagination.</summary>
    public IAsyncEnumerable<Cdr> StreamAsync(CdrFilter? filter = null, int? perPage = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return StreamAsync<Cdr>(ResourceBase, query, new PageRequest { PerPage = perPage }, cancellationToken);
    }
}
