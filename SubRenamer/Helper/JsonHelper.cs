using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace SubRenamer.Helper;

// https://github.com/dotnet/runtimelab/issues/635
// https://github.com/dotnet/runtime/issues/63843
[UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "<Pending>")]
public partial class JsonHelper
{
    private static JsonSerializerOptions JsonOptions => new()
    {
        PropertyNameCaseInsensitive = true,
        AllowTrailingCommas = true,
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,

        TypeInfoResolver = new DefaultJsonTypeInfoResolver(),

        Converters =
        {
            // Write enum value as string
            new JsonStringEnumConverter(),

            new CustomDateTimeConverter("yyyy/MM/dd HH:mm:ss"),
        },

        // ignoring policy
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull,
        IgnoreReadOnlyProperties = true,
        IgnoreReadOnlyFields = true,
    };


    /// <summary>
    /// Parse object to JSON string
    /// </summary>
    public static string ToJson<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, typeof(T), JsonOptions);
    }


    /// <summary>
    /// Parse JSON from a stream
    /// </summary>
    public static async Task<T?> ParseJsonAsync<T>(Stream stream)
    {
        return await JsonSerializer.DeserializeAsync<T>(stream, JsonOptions);
    }
    
    public static T? ParseJsonSync<T>(Stream stream)
    {
        return JsonSerializer.Deserialize<T>(stream, JsonOptions);
    }


    /// <summary>
    /// Writes an object value to JSON file
    /// </summary>
    public static async Task WriteJsonAsync(string jsonFilePath, object? value, CancellationToken token = default)
    {
        var json = JsonSerializer.Serialize(value, JsonOptions);

        await File.WriteAllTextAsync(jsonFilePath, json, Encoding.UTF8, token);
    }
    
    public static void WriteJsonSync(string jsonFilePath, object? value)
    {
        var json = JsonSerializer.Serialize(value, JsonOptions);

        File.WriteAllText(jsonFilePath, json, Encoding.UTF8);
    }
}


public class CustomDateTimeConverter(string format) : JsonConverter<DateTime>
{
    private readonly string Format = format;


    public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
    {
        writer.WriteStringValue(date.ToString(Format));
    }


    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString() ?? "", Format, null);
    }
}
