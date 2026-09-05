using System.Text.Json.Serialization;
using Ampeco.Sdk.Internal;
using Ampeco.Sdk.Models;

namespace Ampeco.Sdk;

/// <summary>
/// Charging session operations (sessions v1.0) and session actions.
/// </summary>
public sealed class SessionsClient : ResourceClient
{
    internal SessionsClient(ApiConnection connection) : base(connection)
    {
    }

    private const string ResourceBase = "resources/sessions/v1.0";
    private const string ActionBase = "actions/session/v1.0";

    /// <summary>Fetches a charging session by id.</summary>
    /// <param name="sessionId">Session id.</param>
    /// <param name="query">Optional expansions applied to the returned session.</param>
    public Task<Session> GetAsync(string sessionId, SessionQuery? query = null, CancellationToken cancellationToken = default)
        => GetAsync<Session>($"{ResourceBase}/{sessionId}", BuildQuery(null, query), cancellationToken);

    /// <summary>Fetches a single page of charging sessions.</summary>
    /// <param name="filter">Optional filters (serialized as deepObject query parameters).</param>
    /// <param name="query">Optional expansions applied to the returned sessions.</param>
    /// <param name="pageRequest">Paging options; defaults to cursor pagination from the first page.</param>
    public Task<Page<Session>> GetPageAsync(SessionFilter? filter = null, SessionQuery? query = null, PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
        => GetPageAsync<Session>(ResourceBase, BuildQuery(filter, query), pageRequest, cancellationToken);

    /// <summary>Iterates over all charging sessions matching the filter, transparently following cursor pagination.</summary>
    public IAsyncEnumerable<Session> StreamAsync(SessionFilter? filter = null, SessionQuery? query = null, int? perPage = null, CancellationToken cancellationToken = default)
        => StreamAsync<Session>(ResourceBase, BuildQuery(filter, query), new PageRequest { PerPage = perPage }, cancellationToken);

    /// <summary>Replaces the custom fields attached to a session.</summary>
    /// <param name="sessionId">Session id.</param>
    /// <param name="customFields">Custom fields to set. Pass an empty dictionary to clear all.</param>
    public Task<Session> UpdateCustomFieldsAsync(string sessionId, IReadOnlyDictionary<string, object?> customFields, CancellationToken cancellationToken = default)
        => Connection.PatchAsync<Session>($"{ResourceBase}/{sessionId}", new CustomFieldsBody(customFields), cancellationToken: cancellationToken);

    /// <summary>Changes the tariff applied to a session (only before billing completes).</summary>
    public Task ChangeTariffAsync(string sessionId, long tariffId, CancellationToken cancellationToken = default)
        => SendNoContentAsync(HttpMethod.Post, $"{ActionBase}/{sessionId}/change-tariff", new ChangeTariffRequest(tariffId), cancellationToken);

    /// <summary>Assigns a session to a user (or unassigns when <c>null</c>).</summary>
    public Task AssignUserAsync(string sessionId, long? userId, CancellationToken cancellationToken = default)
        => SendNoContentAsync(HttpMethod.Post, $"{ActionBase}/{sessionId}/assign-user", new AssignUserRequest(userId), cancellationToken);

    /// <summary>Retries a failed payment for a session.</summary>
    public Task RetryPaymentAsync(string sessionId, CancellationToken cancellationToken = default)
        => SendNoContentAsync(HttpMethod.Post, $"{ActionBase}/{sessionId}/retry-payment", cancellationToken: cancellationToken);

    private static QueryBuilder BuildQuery(SessionFilter? filter, SessionQuery? query = null)
    {
        var qb = new QueryBuilder();
        qb.AddFilter(filter);
        qb.Add("withClockAlignedEnergyConsumption", query?.WithClockAlignedEnergyConsumption);
        qb.Add("clockAlignedInterval", query?.ClockAlignedInterval);
        qb.Add("withAuthorization", query?.WithAuthorization);
        qb.Add("withPriceBreakdown", query?.WithPriceBreakdown);
        qb.Add("withChargingPeriods", query?.WithChargingPeriods);
        qb.Add("withChargingPeriodsPriceBreakdown", query?.WithChargingPeriodsPriceBreakdown);
        qb.Add("withDurationBreakdown", query?.WithDurationBreakdown);
        if (query?.IncludeCustomFields == true)
        {
            qb.AddIncludes(["externalAppData"]);
        }

        return qb;
    }

    private sealed record CustomFieldsBody(IReadOnlyDictionary<string, object?> customFields)
    {
        [JsonPropertyName("customFields")]
        public IReadOnlyDictionary<string, object?> CustomFields { get; init; } = customFields;
    }

    private sealed record ChangeTariffRequest(long tariffId)
    {
        [JsonPropertyName("tariffId")]
        public long TariffId { get; init; } = tariffId;
    }

    private sealed record AssignUserRequest(long? userId)
    {
        [JsonPropertyName("userId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? UserId { get; init; } = userId;
    }
}
