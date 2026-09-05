using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Models;

/// <summary>A partner (fleet/owner organization), as returned by the partners v2.0 endpoints.</summary>
public sealed record Partner
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("operatorId")]
    public long OperatorId { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>Localized display name.</summary>
    [JsonPropertyName("translatedName")]
    public IReadOnlyList<TranslatedText>? TranslatedName { get; init; }

    [JsonPropertyName("businessName")]
    public string? BusinessName { get; init; }

    [JsonPropertyName("regNo")]
    public string? RegistrationNumber { get; init; }

    [JsonPropertyName("vatNo")]
    public string? VatNumber { get; init; }

    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>Localized display address.</summary>
    [JsonPropertyName("translatedAddress")]
    public IReadOnlyList<TranslatedText>? TranslatedAddress { get; init; }

    [JsonPropertyName("postcode")]
    public string? PostCode { get; init; }

    [JsonPropertyName("city")]
    public string? City { get; init; }

    /// <summary>Localized display city.</summary>
    [JsonPropertyName("translatedCity")]
    public IReadOnlyList<TranslatedText>? TranslatedCity { get; init; }

    /// <summary>ISO 3166-1 alpha-2 country code.</summary>
    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("region")]
    public string? Region { get; init; }

    /// <summary>Localized display region.</summary>
    [JsonPropertyName("translatedRegion")]
    public IReadOnlyList<TranslatedText>? TranslatedRegion { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }

    [JsonPropertyName("contactDetails")]
    public PartnerContactDetails? ContactDetails { get; init; }

    [JsonPropertyName("notifications")]
    public PartnerNotifications? Notifications { get; init; }

    /// <summary>Monthly platform fee charged to the partner.</summary>
    [JsonPropertyName("monthlyPlatformFee")]
    public decimal? MonthlyPlatformFee { get; init; }

    [JsonPropertyName("receiptsPrefix")]
    public string? ReceiptsPrefix { get; init; }

    [JsonPropertyName("receiptsStartingNumber")]
    public string? ReceiptsStartingNumber { get; init; }

    [JsonPropertyName("invoiceNumberPrefix")]
    public string? InvoiceNumberPrefix { get; init; }

    [JsonPropertyName("startingInvoiceNumber")]
    public string? StartingInvoiceNumber { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<string>? Tags { get; init; }

    /// <summary>Capabilities granted to the partner.</summary>
    [JsonPropertyName("options")]
    public PartnerOptions? Options { get; init; }

    /// <summary>Corporate billing configuration.</summary>
    [JsonPropertyName("corporateBilling")]
    public PartnerCorporateBilling? CorporateBilling { get; init; }

    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }

    [JsonPropertyName("notes")]
    public IReadOnlyList<Note>? Notes { get; init; }
}

/// <summary>Notification preferences of a partner.</summary>
public sealed record PartnerNotifications
{
    [JsonPropertyName("technical")]
    public PartnerTechnicalNotifications? Technical { get; init; }

    [JsonPropertyName("billing")]
    public PartnerBillingNotifications? Billing { get; init; }
}

/// <summary>Technical notification settings of a partner.</summary>
public sealed record PartnerTechnicalNotifications
{
    [JsonPropertyName("chargePointFaults")]
    public bool? ChargePointFaults { get; init; }
}

/// <summary>Billing notification settings of a partner.</summary>
public sealed record PartnerBillingNotifications
{
    [JsonPropertyName("settlementReports")]
    public bool? SettlementReports { get; init; }

    [JsonPropertyName("settlementReportLanguage")]
    public string? SettlementReportLanguage { get; init; }
}

/// <summary>Capabilities granted to a partner.</summary>
public sealed record PartnerOptions
{
    /// <summary>User visibility scope: <c>self_only</c>, <c>own_partners</c>, <c>all</c> or similar.</summary>
    [JsonPropertyName("userVisibility")]
    public string? UserVisibility { get; init; }

    [JsonPropertyName("allowViewingUsersWhoAcceptedInvite")]
    public bool? AllowViewingUsersWhoAcceptedInvite { get; init; }

    [JsonPropertyName("createUsers")]
    public bool? CreateUsers { get; init; }

    [JsonPropertyName("addUserBalance")]
    public bool? AddUserBalance { get; init; }

    [JsonPropertyName("allowViewingAllSessionsOfInvitedUsers")]
    public bool? AllowViewingAllSessionsOfInvitedUsers { get; init; }

    [JsonPropertyName("supplierOnReceipts")]
    public bool? SupplierOnReceipts { get; init; }

    [JsonPropertyName("supplierOnInvoices")]
    public bool? SupplierOnInvoices { get; init; }

    [JsonPropertyName("allowToControlTariffs")]
    public bool? AllowToControlTariffs { get; init; }

    [JsonPropertyName("allowToControlTariffGroups")]
    public bool? AllowToControlTariffGroups { get; init; }

    [JsonPropertyName("allowToControlCpConfigurations")]
    public bool? AllowToControlCpConfigurations { get; init; }

    /// <summary>Settlement report breakdown: <c>by_location</c>, <c>by_charge_point</c> or similar.</summary>
    [JsonPropertyName("settlementReportBreakdown")]
    public string? SettlementReportBreakdown { get; init; }
}

/// <summary>Corporate billing configuration of a partner.</summary>
public sealed record PartnerCorporateBilling
{
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    [JsonPropertyName("monthlyLimit")]
    public decimal? MonthlyLimit { get; init; }

    /// <summary>Billing frequency: <c>monthly</c>, <c>quarterly</c>, <c>semiAnnually</c> or <c>annually</c>.</summary>
    [JsonPropertyName("frequency")]
    public string? Frequency { get; init; }

    [JsonPropertyName("limit")]
    public decimal? Limit { get; init; }

    [JsonPropertyName("discount")]
    public decimal? Discount { get; init; }
}

/// <summary>
/// Payload for creating or updating a partner (partners v2.0). Only set properties are sent.
/// </summary>
public sealed record PartnerWrite
{
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; init; }

    [JsonPropertyName("translatedName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<TranslatedText>? TranslatedName { get; init; }

    [JsonPropertyName("businessName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BusinessName { get; init; }

    [JsonPropertyName("regNo")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RegistrationNumber { get; init; }

    [JsonPropertyName("vatNo")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? VatNumber { get; init; }

    [JsonPropertyName("address")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Address { get; init; }

    [JsonPropertyName("translatedAddress")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<TranslatedText>? TranslatedAddress { get; init; }

    [JsonPropertyName("postcode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PostCode { get; init; }

    [JsonPropertyName("city")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? City { get; init; }

    [JsonPropertyName("translatedCity")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<TranslatedText>? TranslatedCity { get; init; }

    /// <summary>ISO 3166-1 alpha-2 country code.</summary>
    [JsonPropertyName("country")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Country { get; init; }

    [JsonPropertyName("region")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Region { get; init; }

    [JsonPropertyName("translatedRegion")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<TranslatedText>? TranslatedRegion { get; init; }

    [JsonPropertyName("state")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? State { get; init; }

    [JsonPropertyName("contactDetails")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PartnerContactDetails? ContactDetails { get; init; }

    [JsonPropertyName("notifications")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PartnerWriteNotifications? Notifications { get; init; }

    [JsonPropertyName("monthlyPlatformFee")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? MonthlyPlatformFee { get; init; }

    [JsonPropertyName("receiptsPrefix")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ReceiptsPrefix { get; init; }

    [JsonPropertyName("receiptsStartingNumber")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ReceiptsStartingNumber { get; init; }

    [JsonPropertyName("invoiceNumberPrefix")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InvoiceNumberPrefix { get; init; }

    [JsonPropertyName("startingInvoiceNumber")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StartingInvoiceNumber { get; init; }

    [JsonPropertyName("tags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<string>? Tags { get; init; }

    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PartnerOptions? Options { get; init; }

    [JsonPropertyName("corporateBilling")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PartnerCorporateBilling? CorporateBilling { get; init; }

    [JsonPropertyName("externalId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExternalId { get; init; }
}

/// <summary>Notification settings sent when writing a partner.</summary>
public sealed record PartnerWriteNotifications
{
    [JsonPropertyName("technical")]
    public PartnerTechnicalNotifications? Technical { get; init; }

    [JsonPropertyName("billing")]
    public PartnerBillingWriteNotifications? Billing { get; init; }
}

/// <summary>Billing notification settings sent when writing a partner.</summary>
public sealed record PartnerBillingWriteNotifications
{
    [JsonPropertyName("settlementReports")]
    public bool? SettlementReports { get; init; }
}
