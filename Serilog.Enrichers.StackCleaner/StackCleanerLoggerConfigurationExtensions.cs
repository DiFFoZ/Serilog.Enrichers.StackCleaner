using Serilog.Configuration;
using Serilog.Enrichers.StackCleaner.Enrichers;
using StackCleaner;

namespace Serilog.Enrichers.StackCleaner;
public static class StackCleanerLoggerConfigurationExtensions
{
    public static LoggerConfiguration WithStackCleaner(this LoggerEnrichmentConfiguration enrichmentConfiguration,
        StackCleanerConfiguration? stackCleanerConfiguration = null)
    {
        return enrichmentConfiguration.With(new StackCleanerEnricher(stackCleanerConfiguration));
    }

    public static LoggerConfiguration WithStackCleaner(this LoggerEnrichmentConfiguration enrichmentConfiguration,
        bool includeSourceData = true,
        bool warnForHiddenLines = false,
        bool putSourceDataOnNewLine = true,
        bool includeNamespaces = true,
        bool includeILOffset = false,
        bool includeLineData = true,
        bool includeFileData = false,
        bool useTypeAliases = true,
        StackColorFormatType colorFormatting = StackColorFormatType.None)
    {
        var configuration = new StackCleanerConfiguration
        {
            IncludeSourceData = includeSourceData,
            WarnForHiddenLines = warnForHiddenLines,
            PutSourceDataOnNewLine = putSourceDataOnNewLine,
            IncludeNamespaces = includeNamespaces,
            IncludeILOffset = includeILOffset,
            IncludeLineData = includeLineData,
            IncludeFileData = includeFileData,
            UseTypeAliases = useTypeAliases,
            ColorFormatting = colorFormatting
        };

        return enrichmentConfiguration.WithStackCleaner(configuration);
    }
}
