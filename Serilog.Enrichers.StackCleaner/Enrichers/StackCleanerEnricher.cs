using System.Globalization;
using Serilog.Core;
using Serilog.Enrichers.StackCleaner.Helpers;
using Serilog.Events;
using StackCleaner;

namespace Serilog.Enrichers.StackCleaner.Enrichers;
public class StackCleanerEnricher : ILogEventEnricher
{
    private readonly StackTraceCleaner m_StackTraceCleaner;

    public StackCleanerEnricher(StackCleanerConfiguration? configuration)
    {
        if (configuration is null)
        {
            m_StackTraceCleaner = StackTraceCleaner.Default;
            return;
        }

        m_StackTraceCleaner = new StackTraceCleaner(configuration);
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent.Exception is null)
        {
            return;
        }

        using var writer = new StringWriter(CultureInfo.InvariantCulture);
        m_StackTraceCleaner.WriteToTextWriter(logEvent.Exception, writer);

        logEvent.Exception.SetStackTraceString(writer.ToString());
    }
}
