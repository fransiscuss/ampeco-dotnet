using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ampeco.Sdk.Internal;

internal static class Json
{
    internal static readonly JsonSerializerOptions Serializer = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false),
            new LenientDateTimeOffsetConverter(),
            new LenientNullableDateTimeOffsetConverter(),
            new LenientDateOnlyConverter(),
            new LenientNullableDateOnlyConverter(),
            new LenientTimeOnlyConverter(),
            new LenientNullableTimeOnlyConverter(),
        },
    };
}

/// <summary>
/// Parses timestamps the API returns in mixed formats: ISO 8601 with or without
/// offsets ("2026-03-10T06:05:56+00:00", "2026-03-10T06:05:56Z", "2026-03-10").
/// </summary>
internal sealed class LenientDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    private const string Roundtrip = "O";
    private static readonly string[] FallbackFormats =
    [
        "yyyy-MM-dd'T'HH:mm:ss.FFK",
        "yyyy-MM-dd'T'HH:mm:ss.FFKZZ",
        "yyyy-MM-dd",
    ];

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? throw new JsonException("Expected a timestamp string.");

        if (DateTimeOffset.TryParseExact(value, Roundtrip, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out var result) ||
            DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal, out result))
        {
            return result;
        }

        foreach (var format in FallbackFormats)
        {
            if (DateTimeOffset.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out result))
            {
                return result;
            }
        }

        throw new JsonException($"'{value}' is not a supported timestamp format.");
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToUniversalTime().ToString(Roundtrip, CultureInfo.InvariantCulture));
}

/// <summary>Nullable timestamps: empty strings are treated as null.</summary>
internal sealed class LenientNullableDateTimeOffsetConverter : JsonConverter<DateTimeOffset?>
{
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        var value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return new LenientDateTimeOffsetConverter().Read(ref reader, typeof(DateTimeOffset), options);
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToUniversalTime());
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}

/// <summary>
/// Parses calendar dates returned either as "yyyy-MM-dd" or as full timestamps
/// (the date part is used).
/// </summary>
internal sealed class LenientDateOnlyConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? throw new JsonException("Expected a date string.");

        if (DateOnly.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            return date;
        }

        if (DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal, out var dto))
        {
            return DateOnly.FromDateTime(dto.ToUniversalTime().DateTime);
        }

        throw new JsonException($"'{value}' is not a supported date format.");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
}

/// <summary>Nullable dates: empty strings are treated as null.</summary>
internal sealed class LenientNullableDateOnlyConverter : JsonConverter<DateOnly?>
{
    public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        var value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return new LenientDateOnlyConverter().Read(ref reader, typeof(DateOnly), options);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}

/// <summary>
/// Parses times of day returned as "HH:mm", "HH:mm:ss" or within full timestamps
/// (the time part is used).
/// </summary>
internal sealed class LenientTimeOnlyConverter : JsonConverter<TimeOnly>
{
    private static readonly string[] Formats = ["HH:mm", "HH:mm:ss", "HH:mm:ss.FFF"];

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? throw new JsonException("Expected a time string.");

        foreach (var format in Formats)
        {
            if (TimeOnly.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
            {
                return time;
            }
        }

        if (DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal, out var dto))
        {
            return TimeOnly.FromDateTime(dto.ToUniversalTime().DateTime);
        }

        throw new JsonException($"'{value}' is not a supported time format.");
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString("HH:mm:ss", CultureInfo.InvariantCulture));
}

/// <summary>Nullable times of day: empty strings are treated as null.</summary>
internal sealed class LenientNullableTimeOnlyConverter : JsonConverter<TimeOnly?>
{
    public override TimeOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        var value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return new LenientTimeOnlyConverter().Read(ref reader, typeof(TimeOnly), options);
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToString("HH:mm:ss", CultureInfo.InvariantCulture));
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
