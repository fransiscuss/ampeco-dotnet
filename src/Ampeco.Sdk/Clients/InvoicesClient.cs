using Ampeco.Sdk.Internal;
using Ampeco.Sdk.Models;

namespace Ampeco.Sdk;

/// <summary>
/// Read-only access to invoices (invoices v1.0).
/// </summary>
public sealed class InvoicesClient : ResourceClient
{
    internal InvoicesClient(ApiConnection connection) : base(connection)
    {
    }

    private const string ResourceBase = "resources/invoices/v1.0";

    /// <summary>Fetches an invoice by id.</summary>
    public Task<Invoice> GetAsync(long invoiceId, CancellationToken cancellationToken = default)
        => GetAsync<Invoice>($"{ResourceBase}/{invoiceId}", cancellationToken: cancellationToken);

    /// <summary>Fetches a single page of invoices.</summary>
    /// <param name="filter">Optional filters (serialized as deepObject query parameters).</param>
    /// <param name="pageRequest">Paging options; defaults to cursor pagination from the first page.</param>
    public Task<Page<Invoice>> GetPageAsync(InvoiceFilter? filter = null, PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return GetPageAsync<Invoice>(ResourceBase, query, pageRequest, cancellationToken);
    }

    /// <summary>Iterates over all invoices matching the filter, transparently following cursor pagination.</summary>
    public IAsyncEnumerable<Invoice> StreamAsync(InvoiceFilter? filter = null, int? perPage = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return StreamAsync<Invoice>(ResourceBase, query, new PageRequest { PerPage = perPage }, cancellationToken);
    }
}
