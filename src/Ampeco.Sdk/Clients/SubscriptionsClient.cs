using Ampeco.Sdk.Internal;
using Ampeco.Sdk.Models;

namespace Ampeco.Sdk;

/// <summary>
/// Read-only access to user subscriptions (subscriptions v1.0).
/// </summary>
public sealed class SubscriptionsClient : ResourceClient
{
    internal SubscriptionsClient(ApiConnection connection) : base(connection)
    {
    }

    private const string ResourceBase = "resources/subscriptions/v1.0";

    /// <summary>Fetches a subscription by id.</summary>
    public Task<Subscription> GetAsync(long subscriptionId, CancellationToken cancellationToken = default)
        => GetAsync<Subscription>($"{ResourceBase}/{subscriptionId}", cancellationToken: cancellationToken);

    /// <summary>Fetches a single page of subscriptions.</summary>
    /// <param name="filter">Optional filters (serialized as deepObject query parameters).</param>
    /// <param name="pageRequest">Paging options; defaults to cursor pagination from the first page.</param>
    public Task<Page<Subscription>> GetPageAsync(SubscriptionFilter? filter = null, PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return GetPageAsync<Subscription>(ResourceBase, query, pageRequest, cancellationToken);
    }

    /// <summary>Iterates over all subscriptions matching the filter, transparently following cursor pagination.</summary>
    public IAsyncEnumerable<Subscription> StreamAsync(SubscriptionFilter? filter = null, int? perPage = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return StreamAsync<Subscription>(ResourceBase, query, new PageRequest { PerPage = perPage }, cancellationToken);
    }
}
