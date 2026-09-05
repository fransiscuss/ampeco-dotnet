using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Models;

/// <summary>An invoice, as returned by the invoices v1.0 endpoints.</summary>
public sealed record Invoice
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("operatorId")]
    public long OperatorId { get; init; }

    [JsonPropertyName("partnerId")]
    public long? PartnerId { get; init; }

    /// <summary><see cref="ValueSets.InvoiceType"/>.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("number")]
    public string Number { get; init; } = string.Empty;

    [JsonPropertyName("date")]
    public DateTimeOffset Date { get; init; }

    [JsonPropertyName("userId")]
    public long? UserId { get; init; }

    [JsonPropertyName("periodFrom")]
    public DateTimeOffset? PeriodFrom { get; init; }

    [JsonPropertyName("periodTo")]
    public DateTimeOffset? PeriodTo { get; init; }

    /// <summary>Total energy billed, in kWh.</summary>
    [JsonPropertyName("totalEnergy")]
    public decimal? TotalEnergy { get; init; }

    /// <summary>Total invoice amount (including tax).</summary>
    [JsonPropertyName("totalAmount")]
    public decimal TotalAmount { get; init; }

    /// <summary>Tax rate applied, in percent.</summary>
    [JsonPropertyName("taxRate")]
    public decimal TaxRate { get; init; }

    [JsonPropertyName("taxAmount")]
    public decimal TaxAmount { get; init; }

    /// <summary>Service description printed on the invoice.</summary>
    [JsonPropertyName("service")]
    public string Service { get; init; } = string.Empty;

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; init; }

    [JsonPropertyName("unitPrice")]
    public decimal UnitPrice { get; init; }

    /// <summary>Subtotal (excluding tax).</summary>
    [JsonPropertyName("subtotal")]
    public decimal Subtotal { get; init; }

    /// <summary>Url to download the invoice PDF.</summary>
    [JsonPropertyName("downloadUrl")]
    public string? DownloadUrl { get; init; }

    /// <summary><see cref="ValueSets.InvoicePaymentStatus"/>.</summary>
    [JsonPropertyName("paymentStatus")]
    public string? PaymentStatus { get; init; }

    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    /// <summary>Customer printed on the invoice.</summary>
    [JsonPropertyName("client")]
    public InvoiceClient? Client { get; init; }

    /// <summary>Fiscalization details, when fiscalization is enabled.</summary>
    [JsonPropertyName("fiscalization")]
    public InvoiceFiscalization? Fiscalization { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }
}

/// <summary>The customer printed on an invoice.</summary>
public sealed record InvoiceClient
{
    /// <summary><c>individual</c> or <c>company</c>.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("taxId")]
    public string? TaxId { get; init; }

    [JsonPropertyName("taxAdministrationOfficeName")]
    public string? TaxAdministrationOfficeName { get; init; }

    /// <summary>ISO 3166-1 alpha-2 country code.</summary>
    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("region")]
    public string? Region { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }

    [JsonPropertyName("city")]
    public string? City { get; init; }
}

/// <summary>Fiscalization details of an invoice.</summary>
public sealed record InvoiceFiscalization
{
    /// <summary><see cref="ValueSets.FiscalizationStatus"/>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("referenceNumber")]
    public string? ReferenceNumber { get; init; }

    /// <summary>Fiscalization documents available for download.</summary>
    [JsonPropertyName("documents")]
    public IReadOnlyList<InvoiceFiscalizationDocument>? Documents { get; init; }
}

/// <summary>A downloadable fiscalization document.</summary>
public sealed record InvoiceFiscalizationDocument
{
    /// <summary>Document format, e.g. <c>xml</c> or <c>pdf</c>.</summary>
    [JsonPropertyName("format")]
    public string? Format { get; init; }

    [JsonPropertyName("downloadUrl")]
    public string? DownloadUrl { get; init; }
}

/// <summary>A receipt, as returned by the receipts v2.0 endpoints.</summary>
public sealed record Receipt
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("operatorId")]
    public long OperatorId { get; init; }

    [JsonPropertyName("partnerId")]
    public long? PartnerId { get; init; }

    [JsonPropertyName("receiptNo")]
    public string ReceiptNo { get; init; } = string.Empty;

    [JsonPropertyName("issuedOn")]
    public DateOnly IssuedOn { get; init; }

    [JsonPropertyName("userId")]
    public long? UserId { get; init; }

    /// <summary>Total energy covered by the receipt, in kWh.</summary>
    [JsonPropertyName("totalKwh")]
    public decimal? TotalKwh { get; init; }

    [JsonPropertyName("periodStart")]
    public DateTimeOffset? PeriodStart { get; init; }

    [JsonPropertyName("periodEnd")]
    public DateTimeOffset? PeriodEnd { get; init; }

    /// <summary>Total receipt amount.</summary>
    [JsonPropertyName("totalAmount")]
    public decimal TotalAmount { get; init; }

    [JsonPropertyName("tax")]
    public ReceiptTax? Tax { get; init; }

    /// <summary><see cref="ValueSets.InvoicePaymentStatus"/>.</summary>
    [JsonPropertyName("paymentStatus")]
    public string? PaymentStatus { get; init; }

    /// <summary>Url to download the receipt PDF.</summary>
    [JsonPropertyName("downloadUrl")]
    public string? DownloadUrl { get; init; }
}

/// <summary>Tax details of a receipt.</summary>
public sealed record ReceiptTax
{
    [JsonPropertyName("taxId")]
    public long? TaxId { get; init; }

    /// <summary>Tax percentage applied.</summary>
    [JsonPropertyName("taxPercentage")]
    public decimal? TaxPercentage { get; init; }
}

/// <summary>A user subscription, as returned by the subscriptions v1.0 endpoints.</summary>
public sealed record Subscription
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("planId")]
    public long PlanId { get; init; }

    [JsonPropertyName("userId")]
    public long UserId { get; init; }

    [JsonPropertyName("startDate")]
    public DateTimeOffset StartDate { get; init; }

    [JsonPropertyName("endDate")]
    public DateTimeOffset EndDate { get; init; }

    /// <summary><see cref="ValueSets.SubscriptionStatus"/>.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("statusDate")]
    public DateTimeOffset StatusDate { get; init; }

    [JsonPropertyName("startedAt")]
    public DateTimeOffset StartedAt { get; init; }

    [JsonPropertyName("endedAt")]
    public DateTimeOffset EndedAt { get; init; }

    [JsonPropertyName("statusChangedAt")]
    public DateTimeOffset StatusChangedAt { get; init; }

    /// <summary>Remaining allowance for the current billing period (kWh).</summary>
    [JsonPropertyName("remainingAllowance")]
    public SubscriptionRemainingAllowance? RemainingAllowance { get; init; }

    [JsonPropertyName("remainingFreeRenewalPeriods")]
    public int? RemainingFreeRenewalPeriods { get; init; }

    /// <summary>Whether billing for the subscription is handled externally.</summary>
    [JsonPropertyName("shouldUseExternalBilling")]
    public bool? ShouldUseExternalBilling { get; init; }

    [JsonPropertyName("isRenewable")]
    public bool IsRenewable { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }
}

/// <summary>Remaining subscription allowance, split by charger type.</summary>
public sealed record SubscriptionRemainingAllowance
{
    [JsonPropertyName("combined")]
    public decimal? Combined { get; init; }

    [JsonPropertyName("ac")]
    public decimal? Ac { get; init; }

    [JsonPropertyName("dc")]
    public decimal? Dc { get; init; }
}
