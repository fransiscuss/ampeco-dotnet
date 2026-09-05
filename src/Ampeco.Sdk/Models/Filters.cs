using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Models;

/// <summary>
/// Base for listing filters: null properties are not sent to the API.
/// Properties are serialized as deepObject query parameters, e.g. <c>filter[userId]=123</c>.
/// </summary>
public abstract record ListFilterBase;

/// <summary>Filters for the charge-points v2.0 listing.</summary>
public sealed record ChargePointFilter : ListFilterBase
{
    [JsonPropertyName("operatorId")] public long? OperatorId { get; init; }
    [JsonPropertyName("desiredSecurityProfileStatus")] public string? DesiredSecurityProfileStatus { get; init; }
    [JsonPropertyName("evsePhysicalReference")] public string? EvsePhysicalReference { get; init; }
    [JsonPropertyName("networkId")] public string? NetworkId { get; init; }
    [JsonPropertyName("bootNotificationSerialNumber")] public string? BootNotificationSerialNumber { get; init; }
    [JsonPropertyName("modelId")] public long? ModelId { get; init; }
    [JsonPropertyName("vendorId")] public long? VendorId { get; init; }
    [JsonPropertyName("userId")] public long? UserId { get; init; }
    [JsonPropertyName("partnerId")] public long? PartnerId { get; init; }
    [JsonPropertyName("partnerContractId")] public long? PartnerContractId { get; init; }

    /// <summary><see cref="ValueSets.ChargePointType"/>.</summary>
    [JsonPropertyName("type")] public string? Type { get; init; }

    [JsonPropertyName("subOperatorId")] public long? SubOperatorId { get; init; }

    /// <summary>Roaming operator ids to include.</summary>
    [JsonPropertyName("roamingOperatorIds")] public IReadOnlyList<long>? RoamingOperatorIds { get; init; }

    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("locationId")] public long? LocationId { get; init; }
    [JsonPropertyName("circuitId")] public long? CircuitId { get; init; }
    [JsonPropertyName("chargingZoneId")] public long? ChargingZoneId { get; init; }

    /// <summary>Whether the charge point is managed by the operator: "true"/"false".</summary>
    [JsonPropertyName("managedByOperator")] public string? ManagedByOperator { get; init; }

    [JsonPropertyName("externalId")] public string? ExternalId { get; init; }
    [JsonPropertyName("utilityId")] public long? UtilityId { get; init; }
    [JsonPropertyName("tag")] public string? Tag { get; init; }
    [JsonPropertyName("sharingCode")] public string? SharingCode { get; init; }
    [JsonPropertyName("countryStationId")] public string? CountryStationId { get; init; }
    [JsonPropertyName("createdAfter")] public DateTimeOffset? CreatedAfter { get; init; }
    [JsonPropertyName("createdBefore")] public DateTimeOffset? CreatedBefore { get; init; }
    [JsonPropertyName("lastUpdatedAfter")] public DateTimeOffset? LastUpdatedAfter { get; init; }
    [JsonPropertyName("lastUpdatedBefore")] public DateTimeOffset? LastUpdatedBefore { get; init; }

    /// <summary><see cref="ValueSets.ChargePointNetworkStatus"/>.</summary>
    [JsonPropertyName("networkStatus")] public string? NetworkStatus { get; init; }

    [JsonPropertyName("lastNetworkStatusUpdateAfter")] public DateTimeOffset? LastNetworkStatusUpdateAfter { get; init; }
    [JsonPropertyName("lastNetworkStatusUpdateBefore")] public DateTimeOffset? LastNetworkStatusUpdateBefore { get; init; }
}

/// <summary>Filters for the evses v2.1 listing.</summary>
public sealed record EvseFilter : ListFilterBase
{
    [JsonPropertyName("operatorId")] public long? OperatorId { get; init; }
    [JsonPropertyName("chargePointId")] public long? ChargePointId { get; init; }
    [JsonPropertyName("physicalReference")] public string? PhysicalReference { get; init; }
    [JsonPropertyName("externalId")] public string? ExternalId { get; init; }
    [JsonPropertyName("roaming")] public bool? Roaming { get; init; }
    [JsonPropertyName("lastUpdatedAfter")] public DateTimeOffset? LastUpdatedAfter { get; init; }
    [JsonPropertyName("lastUpdatedBefore")] public DateTimeOffset? LastUpdatedBefore { get; init; }
    [JsonPropertyName("hasRoamingTariffIds")] public bool? HasRoamingTariffIds { get; init; }
    [JsonPropertyName("hasTariffGroup")] public bool? HasTariffGroup { get; init; }
    [JsonPropertyName("roamingOperatorIds")] public IReadOnlyList<long>? RoamingOperatorIds { get; init; }
    [JsonPropertyName("parkingSpaceId")] public long? ParkingSpaceId { get; init; }

    /// <summary><see cref="ValueSets.CurrentType"/>.</summary>
    [JsonPropertyName("currentType")] public string? CurrentType { get; init; }
}

/// <summary>Filters for the locations v2.0 listing.</summary>
public sealed record LocationFilter : ListFilterBase
{
    [JsonPropertyName("postCode")] public string? PostCode { get; init; }
    [JsonPropertyName("partnerId")] public long? PartnerId { get; init; }

    /// <summary>ISO 3166-1 alpha-2 country code.</summary>
    [JsonPropertyName("country")] public string? Country { get; init; }

    /// <summary><see cref="ValueSets.Status"/>.</summary>
    [JsonPropertyName("status")] public string? Status { get; init; }

    [JsonPropertyName("externalId")] public string? ExternalId { get; init; }
}

/// <summary>Filters for the users v1.1 listing.</summary>
public sealed record UserFilter : ListFilterBase
{
    [JsonPropertyName("operatorId")] public long? OperatorId { get; init; }
    [JsonPropertyName("userGroupId")] public long? UserGroupId { get; init; }
    [JsonPropertyName("partnerId")] public long? PartnerId { get; init; }
    [JsonPropertyName("externalId")] public string? ExternalId { get; init; }
    [JsonPropertyName("email")] public string? Email { get; init; }
    [JsonPropertyName("lastUpdatedAfter")] public DateTimeOffset? LastUpdatedAfter { get; init; }
    [JsonPropertyName("lastUpdatedBefore")] public DateTimeOffset? LastUpdatedBefore { get; init; }
    [JsonPropertyName("lastActivityBefore")] public DateTimeOffset? LastActivityBefore { get; init; }
    [JsonPropertyName("invoiceDetailsLastUpdatedAfter")] public DateTimeOffset? InvoiceDetailsLastUpdatedAfter { get; init; }
    [JsonPropertyName("invoiceDetailsLastUpdatedBefore")] public DateTimeOffset? InvoiceDetailsLastUpdatedBefore { get; init; }
    [JsonPropertyName("createdAfter")] public DateTimeOffset? CreatedAfter { get; init; }
    [JsonPropertyName("createdBefore")] public DateTimeOffset? CreatedBefore { get; init; }
}

/// <summary>Filters for the sessions listing.</summary>
public sealed record SessionFilter : ListFilterBase
{
    [JsonPropertyName("operatorId")] public long? OperatorId { get; init; }
    [JsonPropertyName("evseId")] public long? EvseId { get; init; }
    [JsonPropertyName("chargePointId")] public long? ChargePointId { get; init; }
    [JsonPropertyName("evsePhysicalReference")] public string? EvsePhysicalReference { get; init; }
    [JsonPropertyName("chargePointNetworkId")] public string? ChargePointNetworkId { get; init; }

    /// <summary><see cref="ValueSets.SessionEndReason"/>.</summary>
    [JsonPropertyName("reason")] public string? Reason { get; init; }

    [JsonPropertyName("chargePointBootNotificationSerialNumber")] public string? ChargePointBootNotificationSerialNumber { get; init; }
    [JsonPropertyName("chargePointBootNotificationVendor")] public string? ChargePointBootNotificationVendor { get; init; }
    [JsonPropertyName("startedAfter")] public DateTimeOffset? StartedAfter { get; init; }
    [JsonPropertyName("startedBefore")] public DateTimeOffset? StartedBefore { get; init; }
    [JsonPropertyName("userId")] public long? UserId { get; init; }

    /// <summary><see cref="ValueSets.SessionStatus"/>.</summary>
    [JsonPropertyName("status")] public string? Status { get; init; }

    [JsonPropertyName("endedAfter")] public DateTimeOffset? EndedAfter { get; init; }
    [JsonPropertyName("endedBefore")] public DateTimeOffset? EndedBefore { get; init; }
    [JsonPropertyName("partnerId")] public long? PartnerId { get; init; }
    [JsonPropertyName("userPartnerId")] public long? UserPartnerId { get; init; }
    [JsonPropertyName("subOperatorId")] public long? SubOperatorId { get; init; }
    [JsonPropertyName("locationId")] public long? LocationId { get; init; }
    [JsonPropertyName("idTag")] public string? IdTag { get; init; }

    /// <summary><see cref="ValueSets.SessionPaymentType"/>.</summary>
    [JsonPropertyName("paymentType")] public string? PaymentType { get; init; }

    [JsonPropertyName("startedOffline")] public bool? StartedOffline { get; init; }

    /// <summary>Selected payment methods, e.g. <c>visa</c>, <c>apple_pay</c>.</summary>
    [JsonPropertyName("selectedPaymentMethod")] public IReadOnlyList<string>? SelectedPaymentMethod { get; init; }

    /// <summary><see cref="ValueSets.PaymentStatus"/>.</summary>
    [JsonPropertyName("paymentStatus")] public string? PaymentStatus { get; init; }

    [JsonPropertyName("corporateBillingPolicyId")] public long? CorporateBillingPolicyId { get; init; }
    [JsonPropertyName("hasCorporateBilling")] public bool? HasCorporateBilling { get; init; }
    [JsonPropertyName("taxId")] public long? TaxId { get; init; }
    [JsonPropertyName("paymentStatusUpdatedBefore")] public DateTimeOffset? PaymentStatusUpdatedBefore { get; init; }
    [JsonPropertyName("paymentStatusUpdatedAfter")] public DateTimeOffset? PaymentStatusUpdatedAfter { get; init; }
    [JsonPropertyName("billingCompletedBefore")] public DateTimeOffset? BillingCompletedBefore { get; init; }
    [JsonPropertyName("billingCompletedAfter")] public DateTimeOffset? BillingCompletedAfter { get; init; }
    [JsonPropertyName("externalAppData")] public string? ExternalAppData { get; init; }
    [JsonPropertyName("receiptId")] public long? ReceiptId { get; init; }

    /// <summary>Authorization source: <c>roaming</c>, <c>local</c> or <c>third_party</c>.</summary>
    [JsonPropertyName("authorizationSource")] public string? AuthorizationSource { get; init; }

    /// <summary><see cref="ValueSets.SessionBillingStatus"/>.</summary>
    [JsonPropertyName("billingStatus")] public string? BillingStatus { get; init; }

    [JsonPropertyName("terminalId")] public long? TerminalId { get; init; }
    [JsonPropertyName("lastUpdatedAfter")] public DateTimeOffset? LastUpdatedAfter { get; init; }
    [JsonPropertyName("lastUpdatedBefore")] public DateTimeOffset? LastUpdatedBefore { get; init; }

    /// <summary>Roaming-related filters.</summary>
    [JsonPropertyName("roaming")] public SessionRoamingFilter? Roaming { get; init; }
}

/// <summary>Roaming-related session filters.</summary>
public sealed record SessionRoamingFilter : ListFilterBase
{
    [JsonPropertyName("roamingOperatorCpoIds")] public IReadOnlyList<long>? RoamingOperatorCpoIds { get; init; }
    [JsonPropertyName("roamingOperatorEmspIds")] public IReadOnlyList<long>? RoamingOperatorEmspIds { get; init; }
    [JsonPropertyName("roamingConnectionIds")] public IReadOnlyList<long>? RoamingConnectionIds { get; init; }
}

/// <summary>Filters for the transactions listing.</summary>
public sealed record TransactionFilter : ListFilterBase
{
    [JsonPropertyName("operatorId")] public long? OperatorId { get; init; }
    [JsonPropertyName("createdAfter")] public DateTimeOffset? CreatedAfter { get; init; }
    [JsonPropertyName("createdBefore")] public DateTimeOffset? CreatedBefore { get; init; }
    [JsonPropertyName("lastUpdatedAfter")] public DateTimeOffset? LastUpdatedAfter { get; init; }
    [JsonPropertyName("lastUpdatedBefore")] public DateTimeOffset? LastUpdatedBefore { get; init; }

    /// <summary>Payment method used, e.g. <c>visa</c>, <c>applepay</c>.</summary>
    [JsonPropertyName("paymentMethod")] public string? PaymentMethod { get; init; }

    /// <summary>Card networks to include.</summary>
    [JsonPropertyName("cardNetwork")] public IReadOnlyList<string>? CardNetwork { get; init; }

    [JsonPropertyName("cardType")] public string? CardType { get; init; }
    [JsonPropertyName("bin")] public string? Bin { get; init; }
    [JsonPropertyName("cardLast4")] public string? CardLast4 { get; init; }
    [JsonPropertyName("fingerprint")] public string? Fingerprint { get; init; }
    [JsonPropertyName("acquirerName")] public string? AcquirerName { get; init; }
    [JsonPropertyName("issuerName")] public string? IssuerName { get; init; }
    [JsonPropertyName("issuerCode")] public string? IssuerCode { get; init; }
    [JsonPropertyName("userId")] public long? UserId { get; init; }
    [JsonPropertyName("invoiceNumber")] public string? InvoiceNumber { get; init; }

    /// <summary><see cref="ValueSets.TransactionStatus"/>.</summary>
    [JsonPropertyName("status")] public string? Status { get; init; }

    /// <summary>Pending reasons to include.</summary>
    [JsonPropertyName("pendingReason")] public IReadOnlyList<string>? PendingReason { get; init; }

    [JsonPropertyName("sessionId")] public long? SessionId { get; init; }
    [JsonPropertyName("ref")] public string? Ref { get; init; }
    [JsonPropertyName("refContains")] public string? RefContains { get; init; }

    /// <summary><see cref="ValueSets.BillingType"/>.</summary>
    [JsonPropertyName("billingType")] public string? BillingType { get; init; }

    [JsonPropertyName("terminalId")] public long? TerminalId { get; init; }
    [JsonPropertyName("finalizedBefore")] public DateTimeOffset? FinalizedBefore { get; init; }
    [JsonPropertyName("finalizedAfter")] public DateTimeOffset? FinalizedAfter { get; init; }
    [JsonPropertyName("totalAmount")] public decimal? TotalAmount { get; init; }
    [JsonPropertyName("voucherId")] public long? VoucherId { get; init; }
    [JsonPropertyName("receiptId")] public long? ReceiptId { get; init; }
    [JsonPropertyName("invoiceId")] public long? InvoiceId { get; init; }

    /// <summary><see cref="ValueSets.TransactionPurchaseType"/> values.</summary>
    [JsonPropertyName("purchaseType")] public IReadOnlyList<string>? PurchaseType { get; init; }

    [JsonPropertyName("subscriptionBillingPeriodId")] public long? SubscriptionBillingPeriodId { get; init; }
    [JsonPropertyName("settledByTransactionId")] public long? SettledByTransactionId { get; init; }
}

/// <summary>Filters for the tariffs listing.</summary>
public sealed record TariffFilter : ListFilterBase
{
    [JsonPropertyName("operatorId")] public long? OperatorId { get; init; }
    [JsonPropertyName("tariffGroupId")] public long? TariffGroupId { get; init; }
    [JsonPropertyName("userId")] public long? UserId { get; init; }
    [JsonPropertyName("type")] public string? Type { get; init; }
    [JsonPropertyName("partnerId")] public long? PartnerId { get; init; }
    [JsonPropertyName("lastUpdatedAfter")] public DateTimeOffset? LastUpdatedAfter { get; init; }
    [JsonPropertyName("lastUpdatedBefore")] public DateTimeOffset? LastUpdatedBefore { get; init; }
}

/// <summary>Filters for the reservations listing.</summary>
public sealed record ReservationFilter : ListFilterBase
{
    [JsonPropertyName("operatorId")] public long? OperatorId { get; init; }
    [JsonPropertyName("evseId")] public long? EvseId { get; init; }
    [JsonPropertyName("userId")] public long? UserId { get; init; }

    /// <summary><see cref="ValueSets.ReservationStatus"/>.</summary>
    [JsonPropertyName("status")] public string? Status { get; init; }

    [JsonPropertyName("reservedFrom")] public DateTimeOffset? ReservedFrom { get; init; }
    [JsonPropertyName("reservedTo")] public DateTimeOffset? ReservedTo { get; init; }
}

/// <summary>Filters for the partners v2.0 listing.</summary>
public sealed record PartnerFilter : ListFilterBase
{
    [JsonPropertyName("operatorId")] public long? OperatorId { get; init; }

    /// <summary>ISO 3166-1 alpha-2 country code.</summary>
    [JsonPropertyName("country")] public string? Country { get; init; }

    [JsonPropertyName("search")] public string? Search { get; init; }
    [JsonPropertyName("regNo")] public string? RegistrationNumber { get; init; }
    [JsonPropertyName("tag")] public string? Tag { get; init; }
    [JsonPropertyName("hasRoamingOperator")] public bool? HasRoamingOperator { get; init; }
    [JsonPropertyName("roamingOperatorId")] public long? RoamingOperatorId { get; init; }
    [JsonPropertyName("externalId")] public string? ExternalId { get; init; }
    [JsonPropertyName("lastUpdatedAfter")] public DateTimeOffset? LastUpdatedAfter { get; init; }
    [JsonPropertyName("lastUpdatedBefore")] public DateTimeOffset? LastUpdatedBefore { get; init; }
    [JsonPropertyName("createdAfter")] public DateTimeOffset? CreatedAfter { get; init; }
    [JsonPropertyName("createdBefore")] public DateTimeOffset? CreatedBefore { get; init; }
}

/// <summary>Filters for the CDRs v2.0 listing.</summary>
public sealed record CdrFilter : ListFilterBase
{
    [JsonPropertyName("startDateTimeFrom")] public DateTimeOffset? StartDateTimeFrom { get; init; }
    [JsonPropertyName("startDateTimeTo")] public DateTimeOffset? StartDateTimeTo { get; init; }
    [JsonPropertyName("endDateTimeFrom")] public DateTimeOffset? EndDateTimeFrom { get; init; }
    [JsonPropertyName("endDateTimeTo")] public DateTimeOffset? EndDateTimeTo { get; init; }
    [JsonPropertyName("operatorId")] public long? OperatorId { get; init; }
    [JsonPropertyName("platformId")] public long? PlatformId { get; init; }
    [JsonPropertyName("roamingId")] public string? RoamingId { get; init; }
    [JsonPropertyName("credit")] public bool? Credit { get; init; }
    [JsonPropertyName("isLocal")] public bool? IsLocal { get; init; }
    [JsonPropertyName("receivedAfter")] public DateTimeOffset? ReceivedAfter { get; init; }
    [JsonPropertyName("receivedBefore")] public DateTimeOffset? ReceivedBefore { get; init; }
    [JsonPropertyName("sentAfter")] public DateTimeOffset? SentAfter { get; init; }
    [JsonPropertyName("sentBefore")] public DateTimeOffset? SentBefore { get; init; }

    /// <summary>Delivery response: <c>success</c> or <c>fail</c>.</summary>
    [JsonPropertyName("deliveryResponse")] public string? DeliveryResponse { get; init; }

    [JsonPropertyName("sessionId")] public long? SessionId { get; init; }
    [JsonPropertyName("externalId")] public string? ExternalId { get; init; }
}

/// <summary>Filters for the invoices v1.0 listing.</summary>
public sealed record InvoiceFilter : ListFilterBase
{
    [JsonPropertyName("operatorId")] public long? OperatorId { get; init; }
    [JsonPropertyName("issuedFrom")] public string? IssuedFrom { get; init; }
    [JsonPropertyName("issuedTo")] public string? IssuedTo { get; init; }
    [JsonPropertyName("externalId")] public string? ExternalId { get; init; }

    /// <summary><see cref="ValueSets.InvoicePaymentStatus"/> values.</summary>
    [JsonPropertyName("paymentStatus")] public IReadOnlyList<string>? PaymentStatus { get; init; }

    /// <summary><see cref="ValueSets.FiscalizationStatus"/> values.</summary>
    [JsonPropertyName("fiscalizationStatus")] public IReadOnlyList<string>? FiscalizationStatus { get; init; }

    [JsonPropertyName("partnerId")] public long? PartnerId { get; init; }
}

/// <summary>Filters for the receipts v2.0 listing.</summary>
public sealed record ReceiptFilter : ListFilterBase
{
    [JsonPropertyName("operatorId")] public long? OperatorId { get; init; }
    [JsonPropertyName("userId")] public long? UserId { get; init; }
    [JsonPropertyName("taxId")] public long? TaxId { get; init; }

    /// <summary><see cref="ValueSets.InvoicePaymentStatus"/>.</summary>
    [JsonPropertyName("paymentStatus")] public string? PaymentStatus { get; init; }

    [JsonPropertyName("partnerId")] public long? PartnerId { get; init; }
    [JsonPropertyName("periodStart")] public DateTimeOffset? PeriodStart { get; init; }
    [JsonPropertyName("periodEnd")] public DateTimeOffset? PeriodEnd { get; init; }
    [JsonPropertyName("issuedFrom")] public DateOnly? IssuedFrom { get; init; }
    [JsonPropertyName("issuedTo")] public DateOnly? IssuedTo { get; init; }
}

/// <summary>Filters for the subscriptions v1.0 listing.</summary>
public sealed record SubscriptionFilter : ListFilterBase
{
    [JsonPropertyName("planId")] public long? PlanId { get; init; }
    [JsonPropertyName("startedAfter")] public DateTimeOffset? StartedAfter { get; init; }
    [JsonPropertyName("startedBefore")] public DateTimeOffset? StartedBefore { get; init; }
    [JsonPropertyName("endedAfter")] public DateTimeOffset? EndedAfter { get; init; }
    [JsonPropertyName("endedBefore")] public DateTimeOffset? EndedBefore { get; init; }
    [JsonPropertyName("endDateFrom")] public DateTimeOffset? EndDateFrom { get; init; }
    [JsonPropertyName("endDateTo")] public DateTimeOffset? EndDateTo { get; init; }

    /// <summary><see cref="ValueSets.SubscriptionStatus"/>.</summary>
    [JsonPropertyName("status")] public string? Status { get; init; }

    [JsonPropertyName("statusChangedAfter")] public DateTimeOffset? StatusChangedAfter { get; init; }
    [JsonPropertyName("statusChangedBefore")] public DateTimeOffset? StatusChangedBefore { get; init; }
    [JsonPropertyName("billedExternally")] public bool? BilledExternally { get; init; }
}
