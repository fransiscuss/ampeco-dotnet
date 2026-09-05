using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Models;

/// <summary>A tariff, as returned by the tariffs v1.0 endpoints.</summary>
public sealed record Tariff
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("operatorId")]
    public long OperatorId { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary><see cref="ValueSets.TariffType"/>.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public TranslatedTextList? Description { get; init; }

    [JsonPropertyName("additionalInformation")]
    public TranslatedTextList? AdditionalInformation { get; init; }

    [JsonPropertyName("learnMoreUrl")]
    public TranslatedTextList? LearnMoreUrl { get; init; }

    /// <summary>Start of "day" pricing in HH:mm, for day/night tariffs.</summary>
    [JsonPropertyName("dayTariffStart")]
    public string? DayTariffStart { get; init; }

    /// <summary>Start of "night" pricing in HH:mm, for day/night tariffs.</summary>
    [JsonPropertyName("nightTariffStart")]
    public string? NightTariffStart { get; init; }

    [JsonPropertyName("pricing")]
    public TariffPricing? Pricing { get; init; }

    [JsonPropertyName("discountTariffSettings")]
    public TariffDiscountSettings? DiscountTariffSettings { get; init; }

    /// <summary>ISO 4217 currency code.</summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    /// <summary>Automatic session stop limits of the tariff.</summary>
    [JsonPropertyName("stopSession")]
    public TariffStopSession? StopSession { get; init; }

    /// <summary>Who the tariff applies to.</summary>
    [JsonPropertyName("restrictions")]
    public TariffRestrictions? Restrictions { get; init; }

    /// <summary>Partner the tariff belongs to, when it is a partner tariff.</summary>
    [JsonPropertyName("partner")]
    public TariffPartner? Partner { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }

    /// <summary>Tariff display texts shown on charge points.</summary>
    [JsonPropertyName("display")]
    public TariffDisplay? Display { get; init; }

    [JsonPropertyName("integrationId")]
    public long? IntegrationId { get; init; }

    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    /// <summary>Whether the tariff supports incremental pre-authorization.</summary>
    [JsonPropertyName("incrementalPreAuthorizationSupported")]
    public bool IncrementalPreAuthorizationSupported { get; init; }
}

/// <summary>Prices of a tariff. Fields are optional because each tariff type uses different combinations.</summary>
public sealed record TariffPricing
{
    [JsonPropertyName("pricePerSession")] public decimal? PricePerSession { get; init; }
    [JsonPropertyName("connectionFee")] public decimal? ConnectionFee { get; init; }
    [JsonPropertyName("pricePerKwh")] public decimal? PricePerKwh { get; init; }
    [JsonPropertyName("dayPricePerKwh")] public decimal? DayPricePerKwh { get; init; }
    [JsonPropertyName("nightPricePerKwh")] public decimal? NightPricePerKwh { get; init; }

    /// <summary>Billed period length in minutes for time-based tariffs (1, 15, 30, 60, 240 or 360).</summary>
    [JsonPropertyName("pricePeriodInMinutes")] public int? PricePeriodInMinutes { get; init; }

    [JsonPropertyName("pricePerPeriod")] public decimal? PricePerPeriod { get; init; }
    [JsonPropertyName("durationFeeLimit")] public decimal? DurationFeeLimit { get; init; }
    [JsonPropertyName("durationFeeCapMinutes")] public int? DurationFeeCapMinutes { get; init; }
    [JsonPropertyName("dayPricePerPeriod")] public decimal? DayPricePerPeriod { get; init; }
    [JsonPropertyName("nightPricePerPeriod")] public decimal? NightPricePerPeriod { get; init; }
    [JsonPropertyName("dayIdleFeePerMinute")] public decimal? DayIdleFeePerMinute { get; init; }
    [JsonPropertyName("nightIdleFeePerMinute")] public decimal? NightIdleFeePerMinute { get; init; }
    [JsonPropertyName("idleFeePerMinute")] public decimal? IdleFeePerMinute { get; init; }
    [JsonPropertyName("idleFeeGracePeriodMinutes")] public decimal? IdleFeeGracePeriodMinutes { get; init; }

    /// <summary>Idle fee grace mode: <c>idle_detection</c> or <c>session_start</c>.</summary>
    [JsonPropertyName("idleFeeGracePeriodMode")] public string? IdleFeeGracePeriodMode { get; init; }

    [JsonPropertyName("idlePricingPeriodInMinutes")] public int? IdlePricingPeriodInMinutes { get; init; }
    [JsonPropertyName("idleFeePeriodStart")] public string? IdleFeePeriodStart { get; init; }
    [JsonPropertyName("idleFeePeriodEnd")] public string? IdleFeePeriodEnd { get; init; }
    [JsonPropertyName("idleFeeLimit")] public decimal? IdleFeeLimit { get; init; }
    [JsonPropertyName("idleFeeCapMinutes")] public int? IdleFeeCapMinutes { get; init; }
    [JsonPropertyName("connectionFeeMinimumSessionDuration")] public int? ConnectionFeeMinimumSessionDuration { get; init; }
    [JsonPropertyName("connectionFeeMinimumSessionEnergy")] public decimal? ConnectionFeeMinimumSessionEnergy { get; init; }
    [JsonPropertyName("durationFeeGracePeriod")] public int? DurationFeeGracePeriod { get; init; }
    [JsonPropertyName("minPrice")] public decimal? MinPrice { get; init; }

    /// <summary>Amount to pre-authorize when a session starts.</summary>
    [JsonPropertyName("preAuthorizeAmount")] public decimal? PreAuthorizeAmount { get; init; }

    /// <summary>Amount added per incremental pre-authorization step.</summary>
    [JsonPropertyName("incrementalPreAuthorizationAmount")] public decimal? IncrementalPreAuthorizationAmount { get; init; }

    /// <summary>Tax id applied to the tariff.</summary>
    [JsonPropertyName("taxID")] public long? TaxId { get; init; }

    /// <summary>Whether the charge point's electricity rate is used for energy cost.</summary>
    [JsonPropertyName("chargePointElectricityRate")] public bool? ChargePointElectricityRate { get; init; }

    [JsonPropertyName("fallbackElectricityRateId")] public long? FallbackElectricityRateId { get; init; }
    [JsonPropertyName("markupPercentagePerKwh")] public decimal? MarkupPercentagePerKwh { get; init; }
    [JsonPropertyName("markupFixedFeePerKwh")] public decimal? MarkupFixedFeePerKwh { get; init; }

    /// <summary>SoC above which the session is considered idle, when reported by the vehicle.</summary>
    [JsonPropertyName("stateOfChargeIdleThreshold")] public decimal? StateOfChargeIdleThreshold { get; init; }

    /// <summary>Average power below which the session is considered idle.</summary>
    [JsonPropertyName("averagePowerIdleThreshold")] public decimal? AveragePowerIdleThreshold { get; init; }
}

/// <summary>Settings for discount-based tariffs.</summary>
public sealed record TariffDiscountSettings
{
    /// <summary>What the discount applies to: <c>base_tariff</c>, <c>specific_tariff</c> or <c>roaming_tariff</c>.</summary>
    [JsonPropertyName("discountReferenceType")] public string? DiscountReferenceType { get; init; }

    [JsonPropertyName("referencedTariffId")] public long? ReferencedTariffId { get; init; }

    /// <summary><c>global</c> or <c>per_element</c>.</summary>
    [JsonPropertyName("discountMode")] public string? DiscountMode { get; init; }

    [JsonPropertyName("discountPercentage")] public decimal? DiscountPercentage { get; init; }

    /// <summary><c>percentage</c> or <c>flat_amount</c>.</summary>
    [JsonPropertyName("discountType")] public string? DiscountType { get; init; }

    [JsonPropertyName("discountValue")] public decimal? DiscountValue { get; init; }

    [JsonPropertyName("discountElements")] public IReadOnlyList<TariffDiscountElement>? DiscountElements { get; init; }
}

/// <summary>A per-element discount of a discount-based tariff.</summary>
public sealed record TariffDiscountElement
{
    /// <summary>Element the discount applies to: <c>flat</c>, <c>connection</c>, <c>energy</c>, <c>charging_time</c>, <c>idle_time</c>, <c>min_price</c> or <c>service_fee</c>.</summary>
    [JsonPropertyName("serviceFeeType")] public string? ServiceFeeType { get; init; }

    [JsonPropertyName("discountType")] public string? DiscountType { get; init; }

    [JsonPropertyName("discountValue")] public decimal? DiscountValue { get; init; }
}

/// <summary>Automatic session stop limits of a tariff.</summary>
public sealed record TariffStopSession
{
    [JsonPropertyName("timeLimitMinutes")] public decimal? TimeLimitMinutes { get; init; }
    [JsonPropertyName("stopWhenEnergyExceedsKwh")] public decimal? StopWhenEnergyExceedsKwh { get; init; }
    [JsonPropertyName("stopWhenSocExceedsPercent")] public int? StopWhenSocExceedsPercent { get; init; }
}

/// <summary>Defines which users a tariff applies to.</summary>
public sealed record TariffRestrictions
{
    [JsonPropertyName("applyToUsersOfChargePointOwner")] public bool? ApplyToUsersOfChargePointOwner { get; init; }
    [JsonPropertyName("applyToUsersOfChargePointPartner")] public bool? ApplyToUsersOfChargePointPartner { get; init; }
    [JsonPropertyName("applyToUsersOfAllRoamingEmsps")] public bool? ApplyToUsersOfAllRoamingEmsps { get; init; }
    [JsonPropertyName("applyToAdHocUsers")] public bool? ApplyToAdHocUsers { get; init; }
    [JsonPropertyName("adHocPreAuthorizeAmount")] public decimal? AdHocPreAuthorizeAmount { get; init; }
    [JsonPropertyName("adHocIncrementalPreAuthorizationAmount")] public decimal? AdHocIncrementalPreAuthorizationAmount { get; init; }
    [JsonPropertyName("adHocStopWhenPreAuthorizedAmountFallsBelow")] public decimal? AdHocStopWhenPreAuthorizedAmountFallsBelow { get; init; }
    [JsonPropertyName("applyToAdHocOperatorIds")] public IReadOnlyList<long>? ApplyToAdHocOperatorIds { get; init; }
    [JsonPropertyName("applyToUsersOfPartners")] public IReadOnlyList<long>? ApplyToUsersOfPartners { get; init; }
    [JsonPropertyName("applyToUsersWithGroups")] public IReadOnlyList<string>? ApplyToUsersWithGroups { get; init; }
    [JsonPropertyName("applyToUserGroupIds")] public IReadOnlyList<long>? ApplyToUserGroupIds { get; init; }
    [JsonPropertyName("applyToUsersWithSubscriptions")] public IReadOnlyList<long>? ApplyToUsersWithSubscriptions { get; init; }
    [JsonPropertyName("applyToAuthorizationMethods")] public IReadOnlyList<string>? ApplyToAuthorizationMethods { get; init; }
    [JsonPropertyName("startDate")] public DateOnly? StartDate { get; init; }
    [JsonPropertyName("endDate")] public DateOnly? EndDate { get; init; }
}

/// <summary>Partner reference on a partner tariff.</summary>
public sealed record TariffPartner
{
    [JsonPropertyName("id")] public long? Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
}

/// <summary>Display texts of a tariff shown on charge point screens.</summary>
public sealed record TariffDisplay
{
    [JsonPropertyName("defaultPriceInformation")] public string? DefaultPriceInformation { get; init; }
    [JsonPropertyName("defaultPriceInformationOffline")] public string? DefaultPriceInformationOffline { get; init; }
    [JsonPropertyName("priceInformation")] public string? PriceInformation { get; init; }
    [JsonPropertyName("priceInformationLocalized")] public TranslatedTextList? PriceInformationLocalized { get; init; }
    [JsonPropertyName("totalCostInformation")] public string? TotalCostInformation { get; init; }
    [JsonPropertyName("totalCostInformationLocalized")] public TranslatedTextList? TotalCostInformationLocalized { get; init; }
    [JsonPropertyName("plainTextModeEnabled")] public bool? PlainTextModeEnabled { get; init; }
}

/// <summary>
/// Payload for creating or updating a tariff (tariffs v1.0). Only set properties are sent.
/// </summary>
public sealed record TariffWrite
{
    [JsonPropertyName("name")] public string? Name { get; init; }

    /// <summary><see cref="ValueSets.TariffType"/>.</summary>
    [JsonPropertyName("type")] public string? Type { get; init; }

    [JsonPropertyName("description")] [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TranslatedTextList? Description { get; init; }

    [JsonPropertyName("additionalInformation")] [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TranslatedTextList? AdditionalInformation { get; init; }

    [JsonPropertyName("learnMoreUrl")] [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TranslatedTextList? LearnMoreUrl { get; init; }

    [JsonPropertyName("dayTariffStart")] [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DayTariffStart { get; init; }

    [JsonPropertyName("nightTariffStart")] [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? NightTariffStart { get; init; }

    [JsonPropertyName("pricing")] [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TariffPricing? Pricing { get; init; }

    [JsonPropertyName("discountTariffSettings")] [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TariffDiscountSettings? DiscountTariffSettings { get; init; }

    /// <summary>ISO 4217 currency code.</summary>
    [JsonPropertyName("currency")] [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Currency { get; init; }

    [JsonPropertyName("stopSession")] [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TariffStopSession? StopSession { get; init; }

    [JsonPropertyName("restrictions")] [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TariffRestrictions? Restrictions { get; init; }

    [JsonPropertyName("display")] [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TariffDisplay? Display { get; init; }

    [JsonPropertyName("integrationId")] [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? IntegrationId { get; init; }

    [JsonPropertyName("externalId")] [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExternalId { get; init; }
}
