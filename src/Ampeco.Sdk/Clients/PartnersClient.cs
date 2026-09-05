using Ampeco.Sdk.Internal;
using Ampeco.Sdk.Models;

namespace Ampeco.Sdk;

/// <summary>
/// Partner operations (partners v2.0).
/// </summary>
public sealed class PartnersClient : ResourceClient
{
    internal PartnersClient(ApiConnection connection) : base(connection)
    {
    }

    private const string ResourceBase = "resources/partners/v2.0";

    /// <summary>Fetches a partner by id.</summary>
    public Task<Partner> GetAsync(long partnerId, CancellationToken cancellationToken = default)
        => GetAsync<Partner>($"{ResourceBase}/{partnerId}", cancellationToken: cancellationToken);

    /// <summary>Fetches a single page of partners.</summary>
    /// <param name="filter">Optional filters (serialized as deepObject query parameters).</param>
    /// <param name="pageRequest">Paging options; defaults to cursor pagination from the first page.</param>
    public Task<Page<Partner>> GetPageAsync(PartnerFilter? filter = null, PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return GetPageAsync<Partner>(ResourceBase, query, pageRequest, cancellationToken);
    }

    /// <summary>Iterates over all partners matching the filter, transparently following cursor pagination.</summary>
    public IAsyncEnumerable<Partner> StreamAsync(PartnerFilter? filter = null, int? perPage = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return StreamAsync<Partner>(ResourceBase, query, new PageRequest { PerPage = perPage }, cancellationToken);
    }

    /// <summary>Creates a partner.</summary>
    public Task<Partner> CreateAsync(PartnerWrite partner, CancellationToken cancellationToken = default)
        => PostAsync<Partner>(ResourceBase, partner, cancellationToken);

    /// <summary>Updates a partner. Only set properties are sent.</summary>
    public Task<Partner> UpdateAsync(long partnerId, PartnerWrite partner, CancellationToken cancellationToken = default)
        => Connection.PatchAsync<Partner>($"{ResourceBase}/{partnerId}", partner, cancellationToken: cancellationToken);

    /// <summary>Deletes a partner.</summary>
    public Task DeleteAsync(long partnerId, CancellationToken cancellationToken = default)
        => base.DeleteAsync($"{ResourceBase}/{partnerId}", cancellationToken);
}
