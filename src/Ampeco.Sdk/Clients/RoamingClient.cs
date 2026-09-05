using Ampeco.Sdk.Internal;
using Ampeco.Sdk.Models;

namespace Ampeco.Sdk;

/// <summary>
/// Roaming operators and roaming connections (roaming-operators / roaming-connections v2.0).
/// </summary>
public sealed class RoamingClient : ResourceClient
{
    private const string OperatorsBase = "resources/roaming-operators/v2.0";
    private const string ConnectionsBase = "resources/roaming-connections/v2.0";

    internal RoamingClient(ApiConnection connection) : base(connection)
    {
        Connections = new RoamingConnectionsClient(connection);
    }

    /// <summary>Roaming connection operations.</summary>
    public RoamingConnectionsClient Connections { get; }

    /// <summary>Fetches a roaming operator by id.</summary>
    public Task<RoamingOperator> GetOperatorAsync(long roamingOperatorId, CancellationToken cancellationToken = default)
        => GetAsync<RoamingOperator>($"{OperatorsBase}/{roamingOperatorId}", cancellationToken: cancellationToken);

    /// <summary>Fetches a single page of roaming operators.</summary>
    /// <param name="filter">Optional filters (serialized as deepObject query parameters).</param>
    /// <param name="pageRequest">Paging options; defaults to cursor pagination from the first page.</param>
    public Task<Page<RoamingOperator>> GetOperatorsPageAsync(RoamingOperatorFilter? filter = null, PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return GetPageAsync<RoamingOperator>(OperatorsBase, query, pageRequest, cancellationToken);
    }

    /// <summary>Iterates over all roaming operators matching the filter, transparently following cursor pagination.</summary>
    public IAsyncEnumerable<RoamingOperator> StreamOperatorsAsync(RoamingOperatorFilter? filter = null, int? perPage = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return StreamAsync<RoamingOperator>(OperatorsBase, query, new PageRequest { PerPage = perPage }, cancellationToken);
    }

    /// <summary>Updates a roaming operator. Only set properties are sent.</summary>
    public Task<RoamingOperator> UpdateOperatorAsync(long roamingOperatorId, RoamingOperatorWrite roamingOperator, CancellationToken cancellationToken = default)
        => Connection.PatchAsync<RoamingOperator>($"{OperatorsBase}/{roamingOperatorId}", roamingOperator, cancellationToken: cancellationToken);

    /// <summary>Roaming connection operations.</summary>
    public sealed class RoamingConnectionsClient : ResourceClient
    {
        internal RoamingConnectionsClient(ApiConnection connection) : base(connection)
        {
        }

        /// <summary>Fetches a roaming connection by id.</summary>
        public Task<RoamingConnection> GetAsync(long roamingConnectionId, CancellationToken cancellationToken = default)
            => GetAsync<RoamingConnection>($"{ConnectionsBase}/{roamingConnectionId}", cancellationToken: cancellationToken);

        /// <summary>Fetches a single page of roaming connections.</summary>
        public Task<Page<RoamingConnection>> GetPageAsync(PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
            => GetPageAsync<RoamingConnection>(ConnectionsBase, query: null, pageRequest, cancellationToken);

        /// <summary>Iterates over all roaming connections, transparently following cursor pagination.</summary>
        public IAsyncEnumerable<RoamingConnection> StreamAsync(int? perPage = null, CancellationToken cancellationToken = default)
            => StreamAsync<RoamingConnection>(ConnectionsBase, query: null, new PageRequest { PerPage = perPage }, cancellationToken);
    }
}

/// <summary>Filters for the roaming-operators v2.0 listing.</summary>
public sealed record RoamingOperatorFilter : ListFilterBase
{
    [System.Text.Json.Serialization.JsonPropertyName("operatorId")]
    public long? OperatorId { get; init; }
}
