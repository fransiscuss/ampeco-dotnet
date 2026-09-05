using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Models;

/// <summary>A charging session, as returned by the sessions v1.0 endpoints.</summary>
public sealed record Session
{
    /// <summary>Session id (string in the API).</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("operatorId")]
    public long OperatorId { get; init; }

    [JsonPropertyName("chargePointId")]
    public long ChargePointId { get; init; }

    [JsonPropertyName("evseId")]
    public long EvseId { get; init; }

    [JsonPropertyName("connectorId")]
    public long? ConnectorId { get; init; }

    /// <summary><see cref="ValueSets.SessionStatus"/>.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    /// <summary><see cref="ValueSets.SessionEndReason"/> — why the session ended.</summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; init; }

    /// <summary>User that the session is billed to, when known.</summary>
    [JsonPropertyName("userId")]
    public long? UserId { get; init; }

    [JsonPropertyName("vehicleId")]
    public long? VehicleId { get; init; }

    [JsonPropertyName("bookingId")]
    public long? BookingId { get; init; }

    [JsonPropertyName("startedAt")]
    public DateTimeOffset StartedAt { get; init; }

    /// <summary>True when the session started while the charge point was offline.</summary>
    [JsonPropertyName("startedOffline")]
    public bool? StartedOffline { get; init; }

    [JsonPropertyName("stoppedAt")]
    public DateTimeOffset? StoppedAt { get; init; }

    /// <summary>Total energy transferred in kWh.</summary>
    [JsonPropertyName("energy")]
    public decimal Energy { get; init; }

    /// <summary>Average power in kW.</summary>
    [JsonPropertyName("powerKw")]
    public decimal? PowerKw { get; init; }

    /// <summary>State of charge reached, when reported by the vehicle/charge point.</summary>
    [JsonPropertyName("socPercent")]
    public decimal? SocPercent { get; init; }

    /// <summary>Session amount, when the session has been priced.</summary>
    [JsonPropertyName("amount")]
    public decimal? Amount { get; init; }

    /// <summary>Total amount including taxes and fees.</summary>
    [JsonPropertyName("totalAmount")]
    public SessionTotalAmount? TotalAmount { get; init; }

    /// <summary>ISO 4217 currency code.</summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    /// <summary><see cref="ValueSets.SessionPaymentType"/>.</summary>
    [JsonPropertyName("paymentType")]
    public string? PaymentType { get; init; }

    [JsonPropertyName("paymentMethodId")]
    public string? PaymentMethodId { get; init; }

    [JsonPropertyName("terminalId")]
    public long? TerminalId { get; init; }

    /// <summary>Energy that was not billed (e.g. covered by a subscription) in kWh.</summary>
    [JsonPropertyName("nonBillableEnergy")]
    public decimal? NonBillableEnergy { get; init; }

    /// <summary><see cref="ValueSets.PaymentStatus"/>.</summary>
    [JsonPropertyName("paymentStatus")]
    public string? PaymentStatus { get; init; }

    [JsonPropertyName("paymentStatusUpdatedAt")]
    public DateTimeOffset? PaymentStatusUpdatedAt { get; init; }

    /// <summary>Authorization record id used to start the session.</summary>
    [JsonPropertyName("authorizationId")]
    public long? AuthorizationId { get; init; }

    /// <summary>Id tag (RFID UID / token) used to authorize the session.</summary>
    [JsonPropertyName("idTag")]
    public string? IdTag { get; init; }

    [JsonPropertyName("idTagLabel")]
    public string? IdTagLabel { get; init; }

    /// <summary>Type of the id tag: <c>rfid</c>, <c>emaid</c> or <c>mac_address</c>.</summary>
    [JsonPropertyName("idTagType")]
    public string? IdTagType { get; init; }

    [JsonPropertyName("extendingSessionId")]
    public long? ExtendingSessionId { get; init; }

    [JsonPropertyName("originalSessionId")]
    public long? OriginalSessionId { get; init; }

    [JsonPropertyName("extendedBySessionId")]
    public long? ExtendedBySessionId { get; init; }

    [JsonPropertyName("externalSessionId")]
    public string? ExternalSessionId { get; init; }

    /// <summary>Energy cost, when the session has been priced.</summary>
    [JsonPropertyName("electricityCost")]
    public decimal? ElectricityCost { get; init; }

    [JsonPropertyName("evsePhysicalReference")]
    public string? EvsePhysicalReference { get; init; }

    [JsonPropertyName("chargePointOperatorRoamingId")]
    public string? ChargePointOperatorRoamingId { get; init; }

    /// <summary><see cref="ValueSets.SessionBillingStatus"/>.</summary>
    [JsonPropertyName("billingStatus")]
    public string? BillingStatus { get; init; }

    [JsonPropertyName("billingCompletedAt")]
    public DateTimeOffset? BillingCompletedAt { get; init; }

    /// <summary>Amount pre-authorized before the session started.</summary>
    [JsonPropertyName("preAuthorizedAmount")]
    public decimal? PreAuthorizedAmount { get; init; }

    /// <summary>Whether incremental pre-authorization is enabled for the session.</summary>
    [JsonPropertyName("incrementalPreAuthorizationEnabled")]
    public bool? IncrementalPreAuthorizationEnabled { get; init; }

    [JsonPropertyName("receiptId")]
    public long? ReceiptId { get; init; }

    [JsonPropertyName("tariffSnapshotId")]
    public long? TariffSnapshotId { get; init; }

    /// <summary>Custom fields attached to the session (when requested via includes).</summary>
    [JsonPropertyName("customFields")]
    public IReadOnlyDictionary<string, JsonElement>? CustomFields { get; init; }

    /// <summary>Discounts applied to the session (when requested via includes).</summary>
    [JsonPropertyName("discounts")]
    public IReadOnlyList<SessionDiscount>? Discounts { get; init; }

    /// <summary>Energy consumption samples (when requested via <c>withClockAlignedEnergyConsumption</c>).</summary>
    [JsonPropertyName("clockAlignedEnergyConsumption")]
    public IReadOnlyList<ClockAlignedEnergyConsumption>? ClockAlignedEnergyConsumption { get; init; }

    /// <summary>Detailed durations (when requested via <c>withDurationBreakdown</c>).</summary>
    [JsonPropertyName("durationBreakdown")]
    public DurationBreakdown? DurationBreakdown { get; init; }

    /// <summary>Price breakdown of the session (when requested via <c>withPriceBreakdown</c>).</summary>
    [JsonPropertyName("priceBreakdown")]
    public IReadOnlyList<SessionPriceBreakdown>? PriceBreakdown { get; init; }

    /// <summary>Charging periods (when requested via <c>withChargingPeriods</c>).</summary>
    [JsonPropertyName("chargingPeriods")]
    public IReadOnlyList<ChargingPeriod>? ChargingPeriods { get; init; }

    /// <summary>Authorization details (when requested via <c>withAuthorization</c>).</summary>
    [JsonPropertyName("authorization")]
    public SessionAuthorization? Authorization { get; init; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTimeOffset? LastUpdatedAt { get; init; }
}

/// <summary>Total amount of a session with tax split.</summary>
public sealed record SessionTotalAmount
{
    [JsonPropertyName("value")]
    public decimal? Value { get; init; }

    [JsonPropertyName("taxValue")]
    public decimal? TaxValue { get; init; }

    [JsonPropertyName("totalValue")]
    public decimal? TotalValue { get; init; }
}

/// <summary>A discount applied to a session.</summary>
public sealed record SessionDiscount
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("amount")]
    public decimal? Amount { get; init; }

    [JsonPropertyName("percent")]
    public decimal? Percent { get; init; }
}

/// <summary>A clock-aligned energy consumption sample.</summary>
public sealed record ClockAlignedEnergyConsumption
{
    [JsonPropertyName("interval")]
    public int? Interval { get; init; }

    [JsonPropertyName("startedAt")]
    public DateTimeOffset? StartedAt { get; init; }

    [JsonPropertyName("endedAt")]
    public DateTimeOffset? EndedAt { get; init; }

    /// <summary>Energy consumed during the interval in kWh.</summary>
    [JsonPropertyName("energyWh")]
    public decimal? EnergyWh { get; init; }
}

/// <summary>A priced component of a session.</summary>
public sealed record SessionPriceBreakdown
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Unit price of the component.</summary>
    [JsonPropertyName("unitPrice")]
    public decimal? UnitPrice { get; init; }

    /// <summary>Quantity the unit price applies to.</summary>
    [JsonPropertyName("quantity")]
    public decimal? Quantity { get; init; }

    /// <summary>Total price of the component.</summary>
    [JsonPropertyName("totalPrice")]
    public decimal? TotalPrice { get; init; }

    /// <summary>Tax amount included in the total.</summary>
    [JsonPropertyName("taxAmount")]
    public decimal? TaxAmount { get; init; }
}

/// <summary>A single charging (or idle) period within a session.</summary>
public sealed record ChargingPeriod
{
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>Energy transferred during the period in Wh.</summary>
    [JsonPropertyName("energy")]
    public long? Energy { get; init; }

    [JsonPropertyName("energyPrecise")]
    public decimal? EnergyPrecise { get; init; }

    [JsonPropertyName("startedAt")]
    public DateTimeOffset? StartedAt { get; init; }

    [JsonPropertyName("stoppedAt")]
    public DateTimeOffset? StoppedAt { get; init; }

    /// <summary>OCPP transaction status reported by the charge point.</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>Whether the period was <c>charging</c> or <c>idle</c>.</summary>
    [JsonPropertyName("chargingState")]
    public string? ChargingState { get; init; }

    [JsonPropertyName("graceTimeEndAt")]
    public DateTimeOffset? GraceTimeEndAt { get; init; }

    /// <summary>Amount charged for the period.</summary>
    [JsonPropertyName("amount")]
    public decimal? Amount { get; init; }
}

/// <summary>Authorization details of a session (when requested via <c>withAuthorization</c>).</summary>
public sealed record SessionAuthorization
{
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    [JsonPropertyName("uid")]
    public string? Uid { get; init; }

    /// <summary>Type of the authorization: <c>rfid</c>, <c>emaid</c>, <c>mac_address</c> or similar.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("label")]
    public string? Label { get; init; }
}

/// <summary>
/// Optional expansions for session listing/read requests.
/// </summary>
public sealed record SessionQuery
{
    /// <summary>Include clock-aligned energy consumption samples.</summary>
    public bool? WithClockAlignedEnergyConsumption { get; init; }

    /// <summary>Interval of the consumption samples in minutes: 15, 30 or 60.</summary>
    public int? ClockAlignedInterval { get; init; }

    /// <summary>Include authorization details of each session.</summary>
    public bool? WithAuthorization { get; init; }

    /// <summary>Include the price breakdown of each session.</summary>
    public bool? WithPriceBreakdown { get; init; }

    /// <summary>Include the charging periods of each session.</summary>
    public bool? WithChargingPeriods { get; init; }

    /// <summary>Include per-period price breakdown (implies charging periods).</summary>
    public bool? WithChargingPeriodsPriceBreakdown { get; init; }

    /// <summary>Include the duration breakdown of each session.</summary>
    public bool? WithDurationBreakdown { get; init; }

    /// <summary>Include custom fields attached to sessions.</summary>
    public bool? IncludeCustomFields { get; init; }
}

/// <summary>
/// Request to start a charging session on an EVSE (or charge point).
/// </summary>
public sealed record StartSessionRequest
{
    /// <summary>User to bill the session to. When omitted, the session is billed to the ad-hoc/tariff flow.</summary>
    [JsonPropertyName("userId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? UserId { get; init; }

    /// <summary>
    /// Payment method id as returned by the user's payment methods listing.
    /// When null the system determines it (balance or subscription).
    /// </summary>
    [JsonPropertyName("paymentMethodId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PaymentMethodId { get; init; }

    /// <summary>Identifier tag for the session (RFID UID / authorization token); stored in the session's <c>idTag</c> field.</summary>
    [JsonPropertyName("idTag")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IdTag { get; init; }

    /// <summary>Connector id to start on. When omitted, the first available connector is used.</summary>
    [JsonPropertyName("connectorId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? ConnectorId { get; init; }

    /// <summary>Booking id to start the session under, when starting a booked session.</summary>
    [JsonPropertyName("bookingId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? BookingId { get; init; }

    /// <summary>Conditions that stop the session automatically.</summary>
    [JsonPropertyName("stopConditions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SessionStopConditions? StopConditions { get; init; }

    /// <summary>Smart charging profile to apply to the session.</summary>
    [JsonPropertyName("chargingProfile")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SmartChargingProfile? ChargingProfile { get; init; }
}

/// <summary>A smart charging profile to apply when starting a session (OCPP).</summary>
public sealed record SmartChargingProfile
{
    [JsonPropertyName("transactionId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? TransactionId { get; init; }

    [JsonPropertyName("stackLevel")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? StackLevel { get; init; }

    /// <summary><see cref="ValueSets.ChargingProfilePurpose"/>.</summary>
    [JsonPropertyName("chargingProfilePurpose")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ChargingProfilePurpose { get; init; }

    /// <summary><see cref="ValueSets.ChargingProfileKind"/>.</summary>
    [JsonPropertyName("chargingProfileKind")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ChargingProfileKind { get; init; }

    /// <summary><see cref="ValueSets.RecurrencyKind"/>.</summary>
    [JsonPropertyName("recurrencyKind")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RecurrencyKind { get; init; }

    [JsonPropertyName("validFrom")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? ValidFrom { get; init; }

    [JsonPropertyName("validTo")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? ValidTo { get; init; }

    [JsonPropertyName("chargingSchedule")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ChargingSchedule? ChargingSchedule { get; init; }
}

/// <summary>A charging schedule of a smart-charging profile.</summary>
public sealed record ChargingSchedule
{
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? Id { get; init; }

    /// <summary>Duration of the schedule in seconds.</summary>
    [JsonPropertyName("duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Duration { get; init; }

    [JsonPropertyName("startSchedule")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? StartSchedule { get; init; }

    /// <summary><see cref="ValueSets.ChargingRateUnit"/>.</summary>
    [JsonPropertyName("chargingRateUnit")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ChargingRateUnit { get; init; }

    [JsonPropertyName("chargingSchedulePeriod")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<ChargingSchedulePeriod>? ChargingSchedulePeriods { get; init; }

    [JsonPropertyName("minChargingRate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? MinChargingRate { get; init; }
}
