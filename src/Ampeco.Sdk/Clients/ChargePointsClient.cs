using System.Text.Json.Serialization;
using Ampeco.Sdk.Internal;
using Ampeco.Sdk.Models;

namespace Ampeco.Sdk;

/// <summary>
/// Charge point operations (charge-points v2.0 resources and charge-point actions).
/// </summary>
public sealed class ChargePointsClient : ResourceClient
{
    internal ChargePointsClient(ApiConnection connection) : base(connection)
    {
    }

    private const string ResourceBase = "resources/charge-points/v2.0";
    private const string ActionBase = "actions/charge-point/v1.0";

    /// <summary>Fetches a charge point by id.</summary>
    /// <param name="chargePointId">Charge point id.</param>
    public Task<ChargePoint> GetAsync(long chargePointId, CancellationToken cancellationToken = default)
        => GetAsync<ChargePoint>($"{ResourceBase}/{chargePointId}", cancellationToken: cancellationToken);

    /// <summary>Fetches a single page of charge points.</summary>
    /// <param name="filter">Optional filters (serialized as deepObject query parameters).</param>
    /// <param name="pageRequest">Paging options; defaults to cursor pagination from the first page.</param>
    public Task<Page<ChargePoint>> GetPageAsync(ChargePointFilter? filter = null, PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return GetPageAsync<ChargePoint>(ResourceBase, query, pageRequest, cancellationToken);
    }

    /// <summary>Iterates over all charge points matching the filter, transparently following cursor pagination.</summary>
    public IAsyncEnumerable<ChargePoint> StreamAsync(ChargePointFilter? filter = null, int? perPage = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return StreamAsync<ChargePoint>(ResourceBase, query, new PageRequest { PerPage = perPage }, cancellationToken);
    }

    /// <summary>Creates a charge point.</summary>
    public Task<ChargePoint> CreateAsync(ChargePointWrite chargePoint, CancellationToken cancellationToken = default)
        => PostAsync<ChargePoint>(ResourceBase, chargePoint, cancellationToken);

    /// <summary>Updates a charge point. Only set properties are sent.</summary>
    public Task<ChargePoint> UpdateAsync(long chargePointId, ChargePointWrite chargePoint, CancellationToken cancellationToken = default)
        => Connection.PatchAsync<ChargePoint>($"{ResourceBase}/{chargePointId}", chargePoint, cancellationToken: cancellationToken);

    /// <summary>Deletes a charge point.</summary>
    public Task DeleteAsync(long chargePointId, CancellationToken cancellationToken = default)
        => base.DeleteAsync($"{ResourceBase}/{chargePointId}", cancellationToken);

    /// <summary>Fetches the live network/hardware status of a charge point and its EVSEs.</summary>
    public Task<ChargePointStatusInfo> GetStatusAsync(long chargePointId, CancellationToken cancellationToken = default)
        => GetAsync<ChargePointStatusInfo>($"{ResourceBase}/{chargePointId}/status", cancellationToken: cancellationToken);

    /// <summary>
    /// Starts a charging session on a specific EVSE. Returns once the command is accepted (HTTP 202).
    /// </summary>
    /// <param name="chargePointId">Charge point id.</param>
    /// <param name="evseId">EVSE (network) id to start charging on.</param>
    /// <param name="request">Optional session parameters (user, payment method, stop conditions, smart charging).</param>
    public Task StartChargingAsync(long chargePointId, long evseId, StartSessionRequest? request = null, CancellationToken cancellationToken = default)
        => SendNoContentAsync(HttpMethod.Post, $"{ActionBase}/{chargePointId}/start/{evseId}", request, cancellationToken);

    /// <summary>
    /// Starts a charging session without specifying an EVSE — the platform picks the first available one.
    /// </summary>
    public Task StartChargingAsync(long chargePointId, StartSessionRequest? request = null, CancellationToken cancellationToken = default)
        => SendNoContentAsync(HttpMethod.Post, $"{ActionBase}/{chargePointId}/start", request, cancellationToken);

    /// <summary>Stops an active charging session.</summary>
    /// <param name="chargePointId">Charge point id.</param>
    /// <param name="sessionId">Session id to stop.</param>
    /// <param name="force">When true, ends the session regardless of the charge point's response.</param>
    public Task StopChargingAsync(long chargePointId, string sessionId, bool? force = null, CancellationToken cancellationToken = default)
        => SendNoContentAsync(HttpMethod.Post, $"{ActionBase}/{chargePointId}/stop/{sessionId}",
            force.HasValue ? new StopRequest { Force = force.Value } : null, cancellationToken);

    /// <summary>Resets a charge point (OCPP reset). Returns the success flag reported by the API.</summary>
    /// <param name="chargePointId">Charge point id.</param>
    /// <param name="resetType"><see cref="ValueSets.ResetType"/>.</param>
    public async Task<bool> ResetAsync(long chargePointId, string resetType, CancellationToken cancellationToken = default)
    {
        var result = await Connection.PostUnwrappedAsync<SuccessResult>(
            $"{ActionBase}/{chargePointId}/reset/{Uri.EscapeDataString(resetType)}",
            cancellationToken: cancellationToken).ConfigureAwait(false);
        return result.Success ?? false;
    }

    /// <summary>Changes the availability of a charge point or one of its connectors (OCPP change availability).</summary>
    /// <param name="chargePointId">Charge point id.</param>
    /// <param name="type"><see cref="ValueSets.AvailabilityType"/>.</param>
    /// <param name="evseNetworkId">Optional connector network id to target a single connector.</param>
    public Task ChangeAvailabilityAsync(long chargePointId, string type, long? evseNetworkId = null, CancellationToken cancellationToken = default)
        => SendNoContentAsync(HttpMethod.Post, $"{ActionBase}/{chargePointId}/change-availability",
            new ChangeAvailabilityRequest { Type = type, EvseNetworkId = evseNetworkId }, cancellationToken);

    /// <summary>Unlocks the connector of an EVSE. Returns the success flag reported by the API.</summary>
    public async Task<bool> UnlockEvseAsync(long chargePointId, long evseId, CancellationToken cancellationToken = default)
    {
        var result = await Connection.PostUnwrappedAsync<SuccessResult>(
            $"{ActionBase}/{chargePointId}/unlock/{evseId}",
            cancellationToken: cancellationToken).ConfigureAwait(false);
        return result.Success ?? false;
    }

    /// <summary>Reserves an EVSE on a charge point for a user.</summary>
    public Task ReserveEvseAsync(long chargePointId, long evseId, ReserveEvseRequest request, CancellationToken cancellationToken = default)
        => SendNoContentAsync(HttpMethod.Post, $"{ActionBase}/{chargePointId}/reserve/{evseId}", request, cancellationToken);

    internal sealed record SuccessResult
    {
        [JsonPropertyName("success")]
        public bool? Success { get; init; }
    }

    private sealed record StopRequest
    {
        [JsonPropertyName("force")]
        public bool Force { get; init; }
    }

    private sealed record ChangeAvailabilityRequest
    {
        /// <summary><see cref="ValueSets.AvailabilityType"/>.</summary>
        [JsonPropertyName("type")]
        public string Type { get; init; } = string.Empty;

        [JsonPropertyName("evseNetworkId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? EvseNetworkId { get; init; }
    }
}
