using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Models;

/// <summary>A payment transaction, as returned by the transactions v1.0 endpoints.</summary>
public sealed record Transaction
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("operatorId")]
    public long OperatorId { get; init; }

    [JsonPropertyName("number")]
    public string? Number { get; init; }

    [JsonPropertyName("date")]
    public DateTimeOffset Date { get; init; }

    [JsonPropertyName("userId")]
    public long? UserId { get; init; }

    [JsonPropertyName("userEmail")]
    public string? UserEmail { get; init; }

    [JsonPropertyName("sessionId")]
    public long? SessionId { get; init; }

    [JsonPropertyName("paymentMethod")]
    public string? PaymentMethod { get; init; }

    [JsonPropertyName("paymentMethodId")]
    public string? PaymentMethodId { get; init; }

    [JsonPropertyName("paymentProcessorId")]
    public long? PaymentProcessorId { get; init; }

    /// <summary><see cref="ValueSets.TransactionPurchaseType"/>.</summary>
    [JsonPropertyName("purchaseResourceType")]
    public string? PurchaseResourceType { get; init; }

    [JsonPropertyName("purchaseResourceId")]
    public long? PurchaseResourceId { get; init; }

    /// <summary>Total transaction amount.</summary>
    [JsonPropertyName("totalAmount")]
    public decimal? TotalAmount { get; init; }

    /// <summary>Amount authorized (pre-authorized) on the payment method.</summary>
    [JsonPropertyName("authorizedAmount")]
    public decimal? AuthorizedAmount { get; init; }

    /// <summary>ISO 4217 currency code.</summary>
    [JsonPropertyName("currency")]
    public string Currency { get; init; } = string.Empty;

    /// <summary><see cref="ValueSets.TransactionStatus"/>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>Reason the transaction is still pending.</summary>
    [JsonPropertyName("pendingReason")]
    public string? PendingReason { get; init; }

    /// <summary>External processor reference.</summary>
    [JsonPropertyName("ref")]
    public string? Ref { get; init; }

    [JsonPropertyName("bankMessage")]
    public string? BankMessage { get; init; }

    [JsonPropertyName("failureReason")]
    public string? FailureReason { get; init; }

    [JsonPropertyName("invoiceId")]
    public long? InvoiceId { get; init; }

    [JsonPropertyName("invoiceNo")]
    public string? InvoiceNo { get; init; }

    [JsonPropertyName("receiptId")]
    public long? ReceiptId { get; init; }

    /// <summary><see cref="ValueSets.BillingType"/>.</summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }

    [JsonPropertyName("finalizedAt")]
    public DateTimeOffset? FinalizedAt { get; init; }

    [JsonPropertyName("terminalId")]
    public long? TerminalId { get; init; }

    [JsonPropertyName("voucherId")]
    public long? VoucherId { get; init; }

    [JsonPropertyName("subscriptionBillingPeriodId")]
    public long? SubscriptionBillingPeriodId { get; init; }

    [JsonPropertyName("settledByTransactionId")]
    public long? SettledByTransactionId { get; init; }

    [JsonPropertyName("cardDetails")]
    public CardDetails? CardDetails { get; init; }

    [JsonPropertyName("paymentOrderReference")]
    public string? PaymentOrderReference { get; init; }
}

/// <summary>
/// Payload for creating a transaction (transactions v1.0). Only set properties are sent.
/// </summary>
public sealed record TransactionWrite
{
    [JsonPropertyName("operatorId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? OperatorId { get; init; }

    [JsonPropertyName("sessionId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? SessionId { get; init; }

    [JsonPropertyName("paymentMethod")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TransactionPaymentMethodWrite? PaymentMethod { get; init; }

    /// <summary>Total transaction amount. Required.</summary>
    [JsonPropertyName("totalAmount")]
    public decimal TotalAmount { get; init; }

    [JsonPropertyName("authorizedAmount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? AuthorizedAmount { get; init; }

    /// <summary><see cref="ValueSets.TransactionStatus"/>. Required.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = ValueSets.TransactionStatus.Pending;

    [JsonPropertyName("ref")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Ref { get; init; }

    [JsonPropertyName("failureReason")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FailureReason { get; init; }

    /// <summary>Whether an invoice should be generated for the transaction.</summary>
    [JsonPropertyName("invoiceRequired")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? InvoiceRequired { get; init; }

    [JsonPropertyName("email")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Email { get; init; }

    [JsonPropertyName("locale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Locale { get; init; }

    [JsonPropertyName("invoiceDetails")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TransactionInvoiceDetailsWrite? InvoiceDetails { get; init; }

    [JsonPropertyName("paymentOrderReference")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PaymentOrderReference { get; init; }
}

/// <summary>Payment method details for a manually created transaction.</summary>
public sealed record TransactionPaymentMethodWrite
{
    /// <summary>Method type: <c>card</c>, <c>bank_transfer</c> or <c>wallet</c>.</summary>
    [JsonPropertyName("methodType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MethodType { get; init; }

    [JsonPropertyName("cardNetwork")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CardNetwork { get; init; }

    /// <summary>Bank transfer type: <c>pse</c> or <c>sepa</c>.</summary>
    [JsonPropertyName("bankTransferType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BankTransferType { get; init; }

    /// <summary>Wallet type: <c>applepay</c>, <c>googlepay</c>, <c>twint</c>, <c>bancontact</c> or <c>other</c>.</summary>
    [JsonPropertyName("walletType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? WalletType { get; init; }

    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; init; }

    [JsonPropertyName("last4")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Last4 { get; init; }

    [JsonPropertyName("expMonth")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExpMonth { get; init; }

    [JsonPropertyName("expYear")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExpYear { get; init; }
}

/// <summary>Invoice details for a manually created transaction.</summary>
public sealed record TransactionInvoiceDetailsWrite
{
    /// <summary><c>individual</c> or <c>company</c>.</summary>
    [JsonPropertyName("invoiceType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InvoiceType { get; init; }

    [JsonPropertyName("individualName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IndividualName { get; init; }

    [JsonPropertyName("individualPersonalId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IndividualPersonalId { get; init; }

    [JsonPropertyName("individualTaxId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IndividualTaxId { get; init; }

    [JsonPropertyName("companyName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CompanyName { get; init; }

    [JsonPropertyName("companyRegNo")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CompanyRegNo { get; init; }

    [JsonPropertyName("companyTaxId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CompanyTaxId { get; init; }

    [JsonPropertyName("companyTaxAdministrationOfficeName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CompanyTaxAdministrationOfficeName { get; init; }

    /// <summary>ISO 3166-1 alpha-2 country code.</summary>
    [JsonPropertyName("country")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Country { get; init; }

    [JsonPropertyName("city")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? City { get; init; }

    [JsonPropertyName("postCode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PostCode { get; init; }

    [JsonPropertyName("address")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Address { get; init; }
}

/// <summary>Result of creating a pre-authorization. The payload depends on the payment processor.</summary>
public sealed record PreAuthorizationResponse
{
    /// <summary>The payment processor that produced this result: <c>worldline</c> or <c>stripe</c>.</summary>
    [JsonPropertyName("processor")]
    public string? Processor { get; init; }

    /// <summary>Worldline hosted-checkout url the customer is redirected to.</summary>
    [JsonPropertyName("redirectUrl")]
    public string? RedirectUrl { get; init; }

    /// <summary>Stripe PaymentIntent client secret used to confirm the pre-authorization client-side.</summary>
    [JsonPropertyName("clientSecret")]
    public string? ClientSecret { get; init; }
}

/// <summary>
/// Request to create a pre-authorization. The processor field selects the payload shape:
/// <c>stripe</c> requires nothing else; <c>worldline</c> supports the additional fields below.
/// </summary>
public sealed record CreatePreAuthorizationRequest
{
    /// <summary>The payment processor of the transaction: <c>worldline</c> or <c>stripe</c>.</summary>
    [JsonPropertyName("processor")]
    public string Processor { get; init; } = ValueSets.PreAuthorizationProcessor.Stripe;

    /// <summary>Worldline payment product id selecting the payment rail (e.g. TWINT, Bancontact).</summary>
    [JsonPropertyName("paymentProductId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PaymentProductId { get; init; }

    /// <summary>Url Worldline redirects the customer back to after the hosted-checkout page.</summary>
    [JsonPropertyName("returnUrl")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ReturnUrl { get; init; }

    /// <summary>Locale used to render the Worldline hosted-checkout page (e.g. <c>en</c>, <c>de</c>).</summary>
    [JsonPropertyName("locale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Locale { get; init; }
}
