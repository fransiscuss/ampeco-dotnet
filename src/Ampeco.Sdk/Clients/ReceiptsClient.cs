using Ampeco.Sdk.Internal;
using Ampeco.Sdk.Models;

namespace Ampeco.Sdk;

/// <summary>
/// Read-only access to receipts (receipts v2.0).
/// </summary>
public sealed class ReceiptsClient : ResourceClient
{
    internal ReceiptsClient(ApiConnection connection) : base(connection)
    {
    }

    private const string ResourceBase = "resources/receipts/v2.0";

    /// <summary>Fetches a receipt by id.</summary>
    public Task<Receipt> GetAsync(long receiptId, CancellationToken cancellationToken = default)
        => GetAsync<Receipt>($"{ResourceBase}/{receiptId}", cancellationToken: cancellationToken);

    /// <summary>Fetches a single page of receipts.</summary>
    /// <param name="filter">Optional filters (serialized as deepObject query parameters).</param>
    /// <param name="pageRequest">Paging options; defaults to cursor pagination from the first page.</param>
    public Task<Page<Receipt>> GetPageAsync(ReceiptFilter? filter = null, PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return GetPageAsync<Receipt>(ResourceBase, query, pageRequest, cancellationToken);
    }

    /// <summary>Iterates over all receipts matching the filter, transparently following cursor pagination.</summary>
    public IAsyncEnumerable<Receipt> StreamAsync(ReceiptFilter? filter = null, int? perPage = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return StreamAsync<Receipt>(ResourceBase, query, new PageRequest { PerPage = perPage }, cancellationToken);
    }
}
