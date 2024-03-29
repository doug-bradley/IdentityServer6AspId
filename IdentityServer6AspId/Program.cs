﻿using IdentityServer6AspId;
using IdentityServer6AspId.Data;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
	.CreateBootstrapLogger();

Log.Information("Starting up");

try
{
	var builder = WebApplication.CreateBuilder(args);

	builder.Host.UseSerilog((ctx, lc) => lc
		.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
		.Enrich.FromLogContext()
		.ReadFrom.Configuration(ctx.Configuration));

	
    var app = builder
		.ConfigureServices()
		.ConfigurePipeline();

	SeedData.SeedUsers(app);
	SeedData.SeedClients(app);
	SeedData.SeedResources(app);
	SeedData.SeedIdentityResources(app);


	app.Run();
}
catch (Exception ex) when (
	// https://github.com/dotnet/runtime/issues/60600
	ex.GetType().Name is not "StopTheHostException"
	&& ex.GetType().Name is not "HostAbortedException"
)
{
	Log.Fatal(ex, "Unhandled exception");
}
finally
{
	Log.Information("Shut down complete");
	Log.CloseAndFlush();
}