using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Models;

/// <summary>Geographic coordinates (WGS84), as returned by the location endpoints.</summary>
public sealed record GeoPosition
{
    /// <summary>Latitude in decimal degrees.</summary>
    [JsonPropertyName("latitude")]
    public double Latitude { get; init; }

    /// <summary>Longitude in decimal degrees.</summary>
    [JsonPropertyName("longitude")]
    public double Longitude { get; init; }
}

/// <summary>A single localized translation entry.</summary>
public sealed record TranslatedText
{
    [JsonPropertyName("locale")]
    public string Locale { get; init; } = string.Empty;

    [JsonPropertyName("translation")]
    public string Translation { get; init; } = string.Empty;
}

/// <summary>
/// A translated text value: a list of locale/translation pairs, e.g. the location name.
/// Serialized as an array of <c>{ "locale": ..., "translation": ... }</c> objects.
/// Accepts a plain string as well (treated as a single entry without locale).
/// </summary>
[JsonConverter(typeof(TranslatedTextListConverter))]
public sealed class TranslatedTextList : List<TranslatedText>
{
    public TranslatedTextList()
    {
    }

    public TranslatedTextList(IEnumerable<TranslatedText> items) : base(items)
    {
    }

    /// <summary>Returns the translation for the given locale, or the first entry.</summary>
    public string? For(string locale = "en") =>
        this.FirstOrDefault(t => string.Equals(t.Locale, locale, StringComparison.OrdinalIgnoreCase))?.Translation
        ?? this.FirstOrDefault()?.Translation;
}

/// <summary>Converts between plain strings and translated-text arrays.</summary>
public sealed class TranslatedTextListConverter : JsonConverter<TranslatedTextList>
{
    public override TranslatedTextList Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var list = new TranslatedTextList();

        if (reader.TokenType == JsonTokenType.Null)
        {
            return list;
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            list.Add(new TranslatedText { Locale = string.Empty, Translation = reader.GetString() ?? string.Empty });
            return list;
        }

        if (reader.TokenType == JsonTokenType.StartObject)
        {
            // Locale map form: { "en": "Hello", "de": null }
            using var doc = JsonDocument.ParseValue(ref reader);
            foreach (var property in doc.RootElement.EnumerateObject())
            {
                if (property.Value.ValueKind == JsonValueKind.String)
                {
                    list.Add(new TranslatedText { Locale = property.Name, Translation = property.Value.GetString() ?? string.Empty });
                }
            }

            return list;
        }

        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected a string, an object of translations or an array of translations.");
        }

        foreach (var element in JsonSerializer.Deserialize<List<TranslatedText>>(ref reader, options) ?? [])
        {
            list.Add(element);
        }

        return list;
    }

    public override void Write(Utf8JsonWriter writer, TranslatedTextList value, JsonSerializerOptions options)
    {
        if (value.Count == 1 && value[0].Locale.Length == 0)
        {
            writer.WriteStringValue(value[0].Translation);
            return;
        }

        JsonSerializer.Serialize(writer, value, options);
    }
}

/// <summary>An open/closed working-hours interval for a single weekday.</summary>
public sealed record WorkingHoursInterval
{
    [JsonPropertyName("from")]
    public string? From { get; init; }

    [JsonPropertyName("to")]
    public string? To { get; init; }
}

/// <summary>Working hours of a location for one weekday.</summary>
public sealed record DayWorkingHours
{
    [JsonPropertyName("isOpen")]
    public bool? IsOpen { get; init; }

    [JsonPropertyName("intervals")]
    public IReadOnlyList<WorkingHoursInterval>? Intervals { get; init; }
}

/// <summary>Weekly working hours of a location.</summary>
public sealed record WorkingHours
{
    [JsonPropertyName("isAlwaysOpen")]
    public bool? IsAlwaysOpen { get; init; }

    [JsonPropertyName("allowChargingOutsideWorkingHours")]
    public bool? AllowChargingOutsideWorkingHours { get; init; }

    [JsonPropertyName("stopSessionOutsideWorkingHours")]
    public bool? StopSessionOutsideWorkingHours { get; init; }

    [JsonPropertyName("alwaysOpenForUserGroupIds")]
    public IReadOnlyList<long>? AlwaysOpenForUserGroupIds { get; init; }

    [JsonPropertyName("hours")]
    public WeeklyHours? Hours { get; init; }
}

/// <summary>Working hours per weekday.</summary>
public sealed record WeeklyHours
{
    [JsonPropertyName("monday")]
    public IReadOnlyList<WorkingHoursInterval>? Monday { get; init; }

    [JsonPropertyName("tuesday")]
    public IReadOnlyList<WorkingHoursInterval>? Tuesday { get; init; }

    [JsonPropertyName("wednesday")]
    public IReadOnlyList<WorkingHoursInterval>? Wednesday { get; init; }

    [JsonPropertyName("thursday")]
    public IReadOnlyList<WorkingHoursInterval>? Thursday { get; init; }

    [JsonPropertyName("friday")]
    public IReadOnlyList<WorkingHoursInterval>? Friday { get; init; }

    [JsonPropertyName("saturday")]
    public IReadOnlyList<WorkingHoursInterval>? Saturday { get; init; }

    [JsonPropertyName("sunday")]
    public IReadOnlyList<WorkingHoursInterval>? Sunday { get; init; }
}

/// <summary>A back-office note attached to a resource (charge point, EVSE, location, user, partner).</summary>
public sealed record Note
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("summary")]
    public string? Summary { get; init; }

    [JsonPropertyName("details")]
    public string? Details { get; init; }

    [JsonPropertyName("pinned")]
    public bool? Pinned { get; init; }

    [JsonPropertyName("createdByAdminId")]
    public long? CreatedByAdminId { get; init; }

    [JsonPropertyName("updatedByAdminId")]
    public long? UpdatedByAdminId { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public DateTimeOffset UpdatedAt { get; init; }
}

/// <summary>Request payload for creating/updating a note.</summary>
public sealed record NoteWrite
{
    /// <summary>Short summary of the note.</summary>
    [JsonPropertyName("summary")]
    public string? Summary { get; init; }

    /// <summary>Full note body.</summary>
    [JsonPropertyName("details")]
    public string? Details { get; init; }

    [JsonPropertyName("pinned")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Pinned { get; init; }
}

/// <summary>Contact person details, grouped by role (administrative, technical, billing).</summary>
public sealed record ContactPerson
{
    /// <summary>Name of the contact person.</summary>
    [JsonPropertyName("contactPerson")]
    public string? Name { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("phone")]
    public string? Phone { get; init; }
}

/// <summary>Contact details of a partner, grouped by role.</summary>
public sealed record PartnerContactDetails
{
    [JsonPropertyName("administrative")]
    public ContactPerson? Administrative { get; init; }

    [JsonPropertyName("technical")]
    public ContactPerson? Technical { get; init; }

    [JsonPropertyName("billing")]
    public ContactPerson? Billing { get; init; }
}

/// <summary>Power capabilities of an EVSE.</summary>
public sealed record PowerOptions
{
    /// <summary>Maximum output voltage in volts.</summary>
    [JsonPropertyName("maxOutputVoltage")]
    public int? MaxOutputVoltage { get; init; }

    /// <summary>Maximum power output in watts.</summary>
    [JsonPropertyName("maxPower")]
    public int? MaxPower { get; init; }

    [JsonPropertyName("maxVoltage")]
    public string? MaxVoltage { get; init; }

    /// <summary>Maximum amperage.</summary>
    [JsonPropertyName("maxAmperage")]
    public decimal? MaxAmperage { get; init; }

    /// <summary>Phase configuration: <c>single_phase</c>, <c>three_phase</c> or <c>split_phase</c>.</summary>
    [JsonPropertyName("phases")]
    public string? Phases { get; init; }

    [JsonPropertyName("phaseRotation")]
    public string? PhaseRotation { get; init; }

    [JsonPropertyName("connectedPhase")]
    public string? ConnectedPhase { get; init; }
}

/// <summary>Optional overrides of the EVSE capabilities reported by the charge point. <c>null</c>: not overridden, <c>true</c>/<c>false</c>: forced.</summary>
public sealed record EvseCapabilityOverrides
{
    [JsonPropertyName("rfidReader")]
    public bool? RfidReader { get; init; }

    [JsonPropertyName("creditCardPayable")]
    public bool? CreditCardPayable { get; init; }

    [JsonPropertyName("contactlessCardSupport")]
    public bool? ContactlessCardSupport { get; init; }

    [JsonPropertyName("debitCardPayable")]
    public bool? DebitCardPayable { get; init; }

    [JsonPropertyName("chipCardSupport")]
    public bool? ChipCardSupport { get; init; }

    [JsonPropertyName("pedTerminal")]
    public bool? PedTerminal { get; init; }

    [JsonPropertyName("remoteStartStop")]
    public bool? RemoteStartStop { get; init; }

    [JsonPropertyName("unlockCapable")]
    public bool? UnlockCapable { get; init; }

    [JsonPropertyName("reservable")]
    public bool? Reservable { get; init; }

    [JsonPropertyName("chargingProfileCapable")]
    public bool? ChargingProfileCapable { get; init; }

    [JsonPropertyName("chargingPreferencesCapable")]
    public bool? ChargingPreferencesCapable { get; init; }

    [JsonPropertyName("startSessionConnectorRequired")]
    public bool? StartSessionConnectorRequired { get; init; }

    [JsonPropertyName("tokenGroupCapable")]
    public bool? TokenGroupCapable { get; init; }
}

/// <summary>Condition that stops a charging session automatically.</summary>
public sealed record SessionStopConditions
{
    /// <summary>Stop when the transferred energy exceeds this value (kWh).</summary>
    [JsonPropertyName("maxEnergyKwh")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? MaxEnergyKwh { get; init; }

    /// <summary>Stop when the session duration exceeds this value (minutes).</summary>
    [JsonPropertyName("maxDurationMinutes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MaxDurationMinutes { get; init; }

    /// <summary>Stop when the vehicle's state of charge exceeds this percentage.</summary>
    [JsonPropertyName("maxSocPercent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MaxSocPercent { get; init; }

    /// <summary>Stop when the session amount exceeds this value.</summary>
    [JsonPropertyName("maxAmount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? MaxAmount { get; init; }
}

/// <summary>Duration breakdown of a charging session (seconds), including grace periods and billable parts.</summary>
public sealed record DurationBreakdown
{
    [JsonPropertyName("chargingDurationSeconds")]
    public long ChargingDurationSeconds { get; init; }

    [JsonPropertyName("billableChargingDurationSeconds")]
    public long BillableChargingDurationSeconds { get; init; }

    [JsonPropertyName("chargingGracePeriodSeconds")]
    public long ChargingGracePeriodSeconds { get; init; }

    [JsonPropertyName("idleDurationSeconds")]
    public long IdleDurationSeconds { get; init; }

    [JsonPropertyName("billableIdleDurationSeconds")]
    public long BillableIdleDurationSeconds { get; init; }

    [JsonPropertyName("idleGracePeriodSeconds")]
    public long IdleGracePeriodSeconds { get; init; }
}

/// <summary>Card details captured with a payment (transactions).</summary>
public sealed record CardDetails
{
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("cardNetwork")]
    public string? CardNetwork { get; init; }

    [JsonPropertyName("last4")]
    public string? Last4 { get; init; }

    [JsonPropertyName("expMonth")]
    public string? ExpMonth { get; init; }

    [JsonPropertyName("expYear")]
    public string? ExpYear { get; init; }

    [JsonPropertyName("token")]
    public string? Token { get; init; }

    [JsonPropertyName("cardType")]
    public string? CardType { get; init; }

    [JsonPropertyName("bin")]
    public string? Bin { get; init; }

    [JsonPropertyName("fingerprint")]
    public string? Fingerprint { get; init; }

    [JsonPropertyName("acquirerName")]
    public string? AcquirerName { get; init; }

    [JsonPropertyName("issuerName")]
    public string? IssuerName { get; init; }

    [JsonPropertyName("issuerCode")]
    public string? IssuerCode { get; init; }

    [JsonPropertyName("tokenizationMethod")]
    public string? TokenizationMethod { get; init; }
}
