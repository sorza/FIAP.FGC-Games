using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Fgc.Api.Utils.JsonLogs
{
    public class JsonArray : ILogEventSink, IDisposable
    {
        readonly StreamWriter _writer;
        readonly ITextFormatter _formatter;
        bool _first = true;

        public JsonArray(string path, ITextFormatter formatter)
        {            
            _writer = new StreamWriter(
                            File.Open(path,
                                      FileMode.Create,
                                      FileAccess.Write,
                                      FileShare.Read))
            { AutoFlush = true };
            _formatter = formatter;
           
            _writer.WriteLine("[");
        }

        public void Emit(LogEvent logEvent)
        {
            if (!_first)
            {
                _writer.WriteLine(",");  
            }
            
            _formatter.Format(logEvent, _writer);
            _first = false;
        }

        public void Dispose()
        {          
            _writer.WriteLine();
            _writer.WriteLine("]");
            _writer.Dispose();
        }
    }
}
