using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Models;

/// <summary>A platform user, as returned by the users v1.1 endpoints.</summary>
public sealed record User
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("operatorId")]
    public long OperatorId { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("emailVerified")]
    public DateTimeOffset? EmailVerified { get; init; }

    [JsonPropertyName("requirePasswordReset")]
    public bool? RequirePasswordReset { get; init; }

    [JsonPropertyName("firstName")]
    public string? FirstName { get; init; }

    [JsonPropertyName("middleName")]
    public string? MiddleName { get; init; }

    [JsonPropertyName("lastName")]
    public string? LastName { get; init; }

    [JsonPropertyName("phone")]
    public string? Phone { get; init; }

    /// <summary>ISO 3166-1 alpha-2 country code.</summary>
    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }

    [JsonPropertyName("city")]
    public string? City { get; init; }

    [JsonPropertyName("postCode")]
    public string? PostCode { get; init; }

    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>License plate number.</summary>
    [JsonPropertyName("vehicleNo")]
    public string? VehicleNo { get; init; }

    [JsonPropertyName("personalId")]
    public string? PersonalId { get; init; }

    [JsonPropertyName("companyName")]
    public string? CompanyName { get; init; }

    [JsonPropertyName("companyTaxId")]
    public string? CompanyTaxId { get; init; }

    [JsonPropertyName("companyAddress")]
    public string? CompanyAddress { get; init; }

    [JsonPropertyName("companyCity")]
    public string? CompanyCity { get; init; }

    [JsonPropertyName("companyPostalCode")]
    public string? CompanyPostalCode { get; init; }

    [JsonPropertyName("companyCountry")]
    public string? CompanyCountry { get; init; }

    [JsonPropertyName("companyReceiptsEnabled")]
    public bool? CompanyReceiptsEnabled { get; init; }

    /// <summary>IETF locale tag, e.g. <c>en</c>.</summary>
    [JsonPropertyName("locale")]
    public string? Locale { get; init; }

    /// <summary>Wallet balance.</summary>
    [JsonPropertyName("balance")]
    public decimal? Balance { get; init; }

    /// <summary><see cref="ValueSets.UserStatus"/>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("userGroupIds")]
    public IReadOnlyList<long>? UserGroupIds { get; init; }

    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    /// <summary>Active subscription id, when the user has one.</summary>
    [JsonPropertyName("subscriptionId")]
    public long? SubscriptionId { get; init; }

    /// <summary>Amount currently due on the user's account.</summary>
    [JsonPropertyName("amountDue")]
    public decimal? AmountDue { get; init; }

    /// <summary>Activity options (e.g. RFID session behavior).</summary>
    [JsonPropertyName("options")]
    public UserOptions? Options { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; init; }

    [JsonPropertyName("lastActivityAt")]
    public DateTimeOffset? LastActivityAt { get; init; }

    /// <summary><see cref="ValueSets.UserActivitySource"/>.</summary>
    [JsonPropertyName("lastActivitySource")]
    public string? LastActivitySource { get; init; }

    [JsonPropertyName("createdBy")]
    public string? CreatedBy { get; init; }

    [JsonPropertyName("receiveNewsAndPromotions")]
    public bool? ReceiveNewsAndPromotions { get; init; }

    [JsonPropertyName("skipPreAuthorization")]
    public bool? SkipPreAuthorization { get; init; }

    [JsonPropertyName("skipUnpaidSessionCheck")]
    public bool? SkipUnpaidSessionCheck { get; init; }
}

/// <summary>Behavior options of a user.</summary>
public sealed record UserOptions
{
    [JsonPropertyName("rfidSessions")]
    public bool? RfidSessions { get; init; }
}

/// <summary>
/// Payload for creating or updating a user (users v1.1).
/// Note: the API expects snake_case names for several personal fields; they are mapped explicitly.
/// Only set properties are sent.
/// </summary>
public sealed record UserWrite
{
    /// <summary>Email of the user. Required when creating.</summary>
    [JsonPropertyName("email")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Email { get; init; }

    /// <summary>Password of the user. Required when creating.</summary>
    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Password { get; init; }

    [JsonPropertyName("requirePasswordReset")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? RequirePasswordReset { get; init; }

    [JsonPropertyName("first_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FirstName { get; init; }

    [JsonPropertyName("middle_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MiddleName { get; init; }

    [JsonPropertyName("last_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LastName { get; init; }

    [JsonPropertyName("phone")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Phone { get; init; }

    /// <summary>ISO 3166-1 alpha-2 country code.</summary>
    [JsonPropertyName("country")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Country { get; init; }

    [JsonPropertyName("state")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? State { get; init; }

    [JsonPropertyName("city")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? City { get; init; }

    [JsonPropertyName("post_code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PostCode { get; init; }

    [JsonPropertyName("address")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Address { get; init; }

    /// <summary>License plate number.</summary>
    [JsonPropertyName("vehicle_no")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? VehicleNo { get; init; }

    [JsonPropertyName("personal_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PersonalId { get; init; }

    [JsonPropertyName("company_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CompanyName { get; init; }

    [JsonPropertyName("company_tax_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CompanyTaxId { get; init; }

    [JsonPropertyName("company_address")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CompanyAddress { get; init; }

    [JsonPropertyName("company_city")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CompanyCity { get; init; }

    [JsonPropertyName("company_postal_code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CompanyPostalCode { get; init; }

    [JsonPropertyName("company_country")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CompanyCountry { get; init; }

    [JsonPropertyName("company_receipts_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CompanyReceiptsEnabled { get; init; }

    /// <summary>IETF locale tag, e.g. <c>en</c>.</summary>
    [JsonPropertyName("locale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Locale { get; init; }

    [JsonPropertyName("userGroupIds")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<long>? UserGroupIds { get; init; }

    [JsonPropertyName("externalId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExternalId { get; init; }

    /// <summary>Behavior options (e.g. RFID session behavior).</summary>
    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UserOptions? Options { get; init; }

    [JsonPropertyName("receiveNewsAndPromotions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ReceiveNewsAndPromotions { get; init; }

    /// <summary>Partner to associate the new user with.</summary>
    [JsonPropertyName("partnerId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? PartnerId { get; init; }

    /// <summary>One-time nonce for pre-hashed password flows.</summary>
    [JsonPropertyName("nonce")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Nonce { get; init; }
}
