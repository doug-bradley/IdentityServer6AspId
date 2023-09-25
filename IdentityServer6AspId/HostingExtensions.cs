using Duende.IdentityServer;
using IdentityServer6AspId.Data;
using IdentityServer6AspId.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityServer6AspId
{
	internal static class HostingExtensions
	{
		public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
		{
			var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
			var connectionString = builder.Configuration.GetConnectionString("LocalIdentity");

			builder.Services.AddRazorPages();

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			builder.Services
				.AddIdentityServer(options =>
				{
					options.Events.RaiseErrorEvents = true;
					options.Events.RaiseInformationEvents = true;
					options.Events.RaiseFailureEvents = true;
					options.Events.RaiseSuccessEvents = true;

					// see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
					options.EmitStaticAudienceClaim = true;
				})

				.AddConfigurationStore(options =>
				{
					//ConfigurationDbContext: used for configuration data such as clients, resources, and scopes
					options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
						sql => sql.MigrationsAssembly(migrationsAssembly));
				})
				.AddOperationalStore(options =>
				{
					//PersistedGrantDbContext: used for dynamic operational data such as authorization codes and refresh tokens
					options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
						sql => sql.MigrationsAssembly(migrationsAssembly));
				})
				.AddAspNetIdentity<ApplicationUser>();

			builder.Services.AddAuthentication()
				.AddGoogle(options =>
				{
					options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

					// register your IdentityServer with Google at https://console.developers.google.com
					// enable the Google+ API
					// set the redirect URI to https://localhost:5001/signin-google
					options.ClientId = "copy client ID from Google here";
					options.ClientSecret = "copy client secret from Google here";
				});

			return builder.Build();
		}

		public static WebApplication ConfigurePipeline(this WebApplication app)
		{
			app.UseSerilogRequestLogging();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
			app.UseRouting();
			app.UseIdentityServer();
			app.UseAuthorization();

			app.MapRazorPages()
				.RequireAuthorization();

			return app;
		}
	}
}