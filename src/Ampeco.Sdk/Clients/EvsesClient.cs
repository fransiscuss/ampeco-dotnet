using Ampeco.Sdk.Internal;
using Ampeco.Sdk.Models;

namespace Ampeco.Sdk;

/// <summary>
/// EVSE operations (evses v2.1 resources and evse actions).
/// </summary>
public sealed class EvsesClient : ResourceClient
{
    internal EvsesClient(ApiConnection connection) : base(connection)
    {
    }

    private const string ResourceBase = "resources/evses/v2.1";
    private const string ActionBase = "actions/evse/v1.0";

    /// <summary>Fetches an EVSE by id.</summary>
    public Task<Evse> GetAsync(long evseId, CancellationToken cancellationToken = default)
        => GetAsync<Evse>($"{ResourceBase}/{evseId}", cancellationToken: cancellationToken);

    /// <summary>Fetches a single page of EVSEs.</summary>
    /// <param name="filter">Optional filters (serialized as deepObject query parameters).</param>
    /// <param name="pageRequest">Paging options; defaults to cursor pagination from the first page.</param>
    public Task<Page<Evse>> GetPageAsync(EvseFilter? filter = null, PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return GetPageAsync<Evse>(ResourceBase, query, pageRequest, cancellationToken);
    }

    /// <summary>Iterates over all EVSEs matching the filter, transparently following cursor pagination.</summary>
    public IAsyncEnumerable<Evse> StreamAsync(EvseFilter? filter = null, int? perPage = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return StreamAsync<Evse>(ResourceBase, query, new PageRequest { PerPage = perPage }, cancellationToken);
    }

    /// <summary>Creates an EVSE under the charge point specified by <see cref="EvseWrite.ChargePointId"/>.</summary>
    public Task<Evse> CreateAsync(EvseWrite evse, CancellationToken cancellationToken = default)
        => PostAsync<Evse>(ResourceBase, evse, cancellationToken);

    /// <summary>Updates an EVSE. Only set properties are sent.</summary>
    public Task<Evse> UpdateAsync(long evseId, EvseWrite evse, CancellationToken cancellationToken = default)
        => Connection.PatchAsync<Evse>($"{ResourceBase}/{evseId}", evse, cancellationToken: cancellationToken);

    /// <summary>Deletes an EVSE.</summary>
    public Task DeleteAsync(long evseId, CancellationToken cancellationToken = default)
        => base.DeleteAsync($"{ResourceBase}/{evseId}", cancellationToken);

    /// <summary>
    /// Starts a charging session on the EVSE. Returns once the command is accepted (HTTP 202).
    /// </summary>
    /// <param name="evseId">EVSE id.</param>
    /// <param name="request">Optional session parameters (user, payment method, stop conditions, smart charging).</param>
    public Task StartChargingAsync(long evseId, StartSessionRequest? request = null, CancellationToken cancellationToken = default)
        => SendNoContentAsync(HttpMethod.Post, $"{ActionBase}/{evseId}/start", request, cancellationToken);
}
