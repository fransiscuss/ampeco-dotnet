using Ampeco.Sdk.Internal;
using Ampeco.Sdk.Models;

namespace Ampeco.Sdk;

/// <summary>
/// Location operations (locations v2.0).
/// </summary>
public sealed class LocationsClient : ResourceClient
{
    internal LocationsClient(ApiConnection connection) : base(connection)
    {
    }

    private const string ResourceBase = "resources/locations/v2.0";

    /// <summary>Fetches a location by id.</summary>
    public Task<Location> GetAsync(long locationId, CancellationToken cancellationToken = default)
        => GetAsync<Location>($"{ResourceBase}/{locationId}", cancellationToken: cancellationToken);

    /// <summary>Fetches a single page of locations.</summary>
    /// <param name="filter">Optional filters (serialized as deepObject query parameters).</param>
    /// <param name="pageRequest">Paging options; defaults to cursor pagination from the first page.</param>
    public Task<Page<Location>> GetPageAsync(LocationFilter? filter = null, PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return GetPageAsync<Location>(ResourceBase, query, pageRequest, cancellationToken);
    }

    /// <summary>Iterates over all locations matching the filter, transparently following cursor pagination.</summary>
    public IAsyncEnumerable<Location> StreamAsync(LocationFilter? filter = null, int? perPage = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return StreamAsync<Location>(ResourceBase, query, new PageRequest { PerPage = perPage }, cancellationToken);
    }

    /// <summary>Creates a location.</summary>
    public Task<Location> CreateAsync(LocationWrite location, CancellationToken cancellationToken = default)
        => PostAsync<Location>(ResourceBase, location, cancellationToken);

    /// <summary>Updates a location. Only set properties are sent.</summary>
    public Task<Location> UpdateAsync(long locationId, LocationWrite location, CancellationToken cancellationToken = default)
        => Connection.PatchAsync<Location>($"{ResourceBase}/{locationId}", location, cancellationToken: cancellationToken);

    /// <summary>Deletes a location.</summary>
    public Task DeleteAsync(long locationId, CancellationToken cancellationToken = default)
        => base.DeleteAsync($"{ResourceBase}/{locationId}", cancellationToken);
}
