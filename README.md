# Serilog.Enrichers.StackCleaner [![Nuget](https://img.shields.io/nuget/dt/Serilog.Enrichers.StackCleaner?style=for-the-badge)](https://www.nuget.org/packages/Serilog.Enrichers.StackCleaner/)

Clears up stack traces to make them much more readable using [StackCleaner by DanielWillett](https://github.com/DanielWillett/StackCleaner).

## Usage
### From the code
`Serilog.Enrichers.StackCleaner` adds `WithStackCleaner` method to the `Enrich`.
```cs
Log.Logger = new LoggerConfiguration()
    .Enrich.WithStackCleaner()
    .WriteTo.Console()
    .CreateLogger();
```

### From `appSettings.json`
```json
{
  "Serilog":
  {
    "Using": [ "Serilog.Enrichers.StackCleaner" ],
    "Enrich": [ "WithStackCleaner" ]
  }
}
```