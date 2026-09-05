using Ampeco.Sdk.Internal;
using Ampeco.Sdk.Models;

namespace Ampeco.Sdk;

/// <summary>
/// Tariff operations (tariffs v1.0).
/// </summary>
public sealed class TariffsClient : ResourceClient
{
    internal TariffsClient(ApiConnection connection) : base(connection)
    {
    }

    private const string ResourceBase = "resources/tariffs/v1.0";

    /// <summary>Fetches a tariff by id.</summary>
    public Task<Tariff> GetAsync(long tariffId, CancellationToken cancellationToken = default)
        => GetAsync<Tariff>($"{ResourceBase}/{tariffId}", cancellationToken: cancellationToken);

    /// <summary>Fetches a single page of tariffs.</summary>
    /// <param name="filter">Optional filters (serialized as deepObject query parameters).</param>
    /// <param name="pageRequest">Paging options; defaults to cursor pagination from the first page.</param>
    public Task<Page<Tariff>> GetPageAsync(TariffFilter? filter = null, PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return GetPageAsync<Tariff>(ResourceBase, query, pageRequest, cancellationToken);
    }

    /// <summary>Iterates over all tariffs matching the filter, transparently following cursor pagination.</summary>
    public IAsyncEnumerable<Tariff> StreamAsync(TariffFilter? filter = null, int? perPage = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return StreamAsync<Tariff>(ResourceBase, query, new PageRequest { PerPage = perPage }, cancellationToken);
    }

    /// <summary>Creates a tariff.</summary>
    public Task<Tariff> CreateAsync(TariffWrite tariff, CancellationToken cancellationToken = default)
        => PostAsync<Tariff>(ResourceBase, tariff, cancellationToken);

    /// <summary>Replaces a tariff entirely (PUT). Unset properties are cleared.</summary>
    public Task<Tariff> ReplaceAsync(long tariffId, TariffWrite tariff, CancellationToken cancellationToken = default)
        => Connection.PutAsync<Tariff>($"{ResourceBase}/{tariffId}", tariff, cancellationToken: cancellationToken);

    /// <summary>Partially updates a tariff. Only set properties are sent.</summary>
    public Task<Tariff> UpdateAsync(long tariffId, TariffWrite tariff, CancellationToken cancellationToken = default)
        => Connection.PatchAsync<Tariff>($"{ResourceBase}/{tariffId}", tariff, cancellationToken: cancellationToken);

    /// <summary>Deletes a tariff.</summary>
    public Task DeleteAsync(long tariffId, CancellationToken cancellationToken = default)
        => base.DeleteAsync($"{ResourceBase}/{tariffId}", cancellationToken);
}
