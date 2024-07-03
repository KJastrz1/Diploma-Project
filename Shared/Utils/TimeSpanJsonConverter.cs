using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Utils;
public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    private const string TimeFormat = @"hh\:mm";

    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return TimeSpan.ParseExact(value, TimeFormat, null);
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(TimeFormat));
    }
}
