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
        },
    };
}
