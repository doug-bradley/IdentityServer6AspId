using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Configuration.EntityFramework;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Storage;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddIdentityServerConfiguration(
		opt =>
		{
			opt.LicenseKey = "<license>";
		}).AddClientConfigurationStore();

builder.Services.AddConfigurationDbContext<ConfigurationDbContext>(
	options =>
	{
		options.ConfigureDbContext = b => b.UseSqlServer(connectionString);
	});


builder.Services.AddIdentityServerConfiguration(builder.Configuration);

var app = builder.Build();
app.MapDynamicClientRegistration().RequireAuthorization("DCR");

app.Run();