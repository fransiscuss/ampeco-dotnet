using Ampeco.Sdk.Internal;
using Ampeco.Sdk.Models;

namespace Ampeco.Sdk;

/// <summary>
/// Payment transaction operations (transactions v1.0) and the pre-authorization action.
/// </summary>
public sealed class TransactionsClient : ResourceClient
{
    internal TransactionsClient(ApiConnection connection) : base(connection)
    {
    }

    private const string ResourceBase = "resources/transactions/v1.0";
    private const string ActionBase = "actions/transaction/v1.0";

    /// <summary>Fetches a transaction by id.</summary>
    public Task<Transaction> GetAsync(long transactionId, CancellationToken cancellationToken = default)
        => GetAsync<Transaction>($"{ResourceBase}/{transactionId}", cancellationToken: cancellationToken);

    /// <summary>Fetches a single page of transactions.</summary>
    /// <param name="filter">Optional filters (serialized as deepObject query parameters).</param>
    /// <param name="pageRequest">Paging options; defaults to cursor pagination from the first page.</param>
    public Task<Page<Transaction>> GetPageAsync(TransactionFilter? filter = null, PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return GetPageAsync<Transaction>(ResourceBase, query, pageRequest, cancellationToken);
    }

    /// <summary>Iterates over all transactions matching the filter, transparently following cursor pagination.</summary>
    public IAsyncEnumerable<Transaction> StreamAsync(TransactionFilter? filter = null, int? perPage = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return StreamAsync<Transaction>(ResourceBase, query, new PageRequest { PerPage = perPage }, cancellationToken);
    }

    /// <summary>Creates a payment transaction (used to record external payments).</summary>
    public Task<Transaction> CreateAsync(TransactionWrite transaction, CancellationToken cancellationToken = default)
        => PostAsync<Transaction>(ResourceBase, transaction, cancellationToken);

    /// <summary>Updates a transaction. Only set properties are sent.</summary>
    public Task<Transaction> UpdateAsync(long transactionId, TransactionWrite transaction, CancellationToken cancellationToken = default)
        => Connection.PatchAsync<Transaction>($"{ResourceBase}/{transactionId}", transaction, cancellationToken: cancellationToken);

    /// <summary>Creates a pre-authorization for a transaction.</summary>
    /// <param name="transactionId">Transaction id.</param>
    /// <param name="request">Processor-specific pre-authorization request.</param>
    public Task<PreAuthorizationResponse> CreatePreAuthorizationAsync(long transactionId, CreatePreAuthorizationRequest request, CancellationToken cancellationToken = default)
        => PostAsync<PreAuthorizationResponse>($"{ActionBase}/{transactionId}/create-pre-authorization", request, cancellationToken);
}
