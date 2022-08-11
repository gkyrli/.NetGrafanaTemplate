using System.Reflection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging((context, loggingBuilder) => loggingBuilder.ClearProviders()).UseSerilog(
    (context, configuration) => configuration.WriteTo
        .GrafanaLoki(
            "https://261933:eyJrIjoiMWJiNGU2ZjAzODU4MzIwNWVjYzMxNDQzZDYzMmU2NTBjODczN2QwNiIsIm4iOiJwdWJsaXNoIiwiaWQiOjY4OTY0N30=@logs-prod-eu-west-0.grafana.net/")
// api/prom/push
        .WriteTo.Console()
);
var serviceName = Assembly.GetAssembly(typeof(Program))?.GetName().Name ?? "Krokos";
var serviceVersion = "1.0.0";

var lokiCredentials = new LokiCredentials(){Login ="261933",Password = "eyJrIjoiMWJiNGU2ZjAzODU4MzIwNWVjYzMxNDQzZDYzMmU2NTBjODczN2QwNiIsIm4iOiJwdWJsaXNoIiwiaWQiOjY4OTY0N30="};

var logger = new LoggerConfiguration()
    .WriteTo.GrafanaLoki(
        "https://logs-prod-eu-west-0.grafana.net/", new List<LokiLabel>(){new LokiLabel(){Key = "app",Value = "sctask"}},credentials:lokiCredentials,batchPostingLimit:100)
    .CreateLogger();
while (true)
{
    logger.Information("The god of the day is {@God}", "odin");
    Thread.Sleep(1000);
}

builder.Services.AddOpenTelemetryTracing(
    (builder) => builder
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault().AddService("example-app"))
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter()
        .AddOtlpExporter(opt => { opt.Endpoint = new Uri("http://localhost:4317"); }));
// Add services to the container.
// builder.Services.AddScoped<Il>(x => new LoggerConfiguration()
//     .WriteTo.GrafanaLoki(
//         "http://localhost:3100")
//     .CreateLogger());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();