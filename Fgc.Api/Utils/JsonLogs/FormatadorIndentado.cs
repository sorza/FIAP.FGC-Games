using Serilog.Events;
using Serilog.Formatting;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fgc.Api.Utils.JsonLogs
{
    public class FormatadorIndentado : ITextFormatter
    {
        static readonly JsonSerializerOptions _opts = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public void Format(LogEvent logEvent, TextWriter output)
        {
            var props = logEvent.Properties.ToDictionary(
                kv => kv.Key,
                kv =>
                {
                    if (kv.Value is ScalarValue sv && sv.Value is string s)
                        return (object)s;
                    if (kv.Value is ScalarValue sv2)
                        return sv2.Value ?? sv2.ToString();
                    return kv.Value.ToString();
                });

            var evt = new
            {
                logEvent.Timestamp,
                Level = logEvent.Level.ToString(),
                Message = logEvent.RenderMessage(),
                Exception = logEvent.Exception?.ToString(),
                Properties = props
            };

            output.WriteLine(JsonSerializer.Serialize(evt, _opts));
        }
    }
}
