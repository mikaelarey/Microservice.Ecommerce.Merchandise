using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Merchandise.Domain.Extensions
{
    public class FlexibleDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        private readonly string[] formats = new[]
        {
            "yyyy-MM-dd HH:mm:ss.FFFFFFF zzz",
            "yyyy-MM-ddTHH:mm:ss.FFFFFFFzzz" // fallback to default
        };

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (DateTimeOffset.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
            {
                return result;
            }

            DateTimeOffset x;
            DateTimeOffset.TryParse(value, out x);

            return x;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(formats[0], CultureInfo.InvariantCulture));
        }
    }

}
