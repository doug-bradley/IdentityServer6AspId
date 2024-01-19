using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Models;
using IdentityServer6AspId.Data;
using IdentityServer6AspId.Models;
using IdentityServer6AspId.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace IdentityServer6AspId
{
	internal static class HostingExtensions
	{
		public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
		{
			var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
			var connectionString = builder.Configuration.GetConnectionString("LocalIdentity");

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowSpecificOrigin",
					b =>
					{
						b.WithOrigins("http://localhost:1234") // Replace with your Vue.js application's origin
							.AllowAnyHeader()
							.AllowAnyMethod()
							.AllowCredentials();
					});
			});

			builder.Services.AddRazorPages();


			builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender, EmailSender>();
			builder.Services.AddTransient<IUserService, UserService>();

			builder.Services
				.AddIdentityServer(options =>
				{
					options.Events.RaiseErrorEvents = true;
					options.Events.RaiseInformationEvents = true;
					options.Events.RaiseFailureEvents = true;
					options.Events.RaiseSuccessEvents = true;
					options.Csp = new CspOptions() { Level = CspLevel.One };
					// see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
					options.EmitStaticAudienceClaim = true;
				})
				.AddConfigurationStore(options =>
				{
					options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
				})
				.AddOperationalStore(options =>
				{
					options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
				})
				.AddAspNetIdentity<ApplicationUser>()
				.AddProfileService<CustomProfileService>()
                .AddAuthorizeInteractionResponseGenerator<TenantInteractionResponseGenerator>();

            builder.Services.AddAAD();
           
            builder.Services.AddGoogle();

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
			app.UseCors("AllowSpecificOrigin"); // Use the CORS policy

			app.Use(async (context, next) =>
			{
				context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; connect-src 'self' wss://localhost:*; script-src https://localhost:5001 'self'  'sha256-fa5rxHhZ799izGRP38+h4ud5QXNT0SFaFlh4eqDumBI=';");
				await next();
			});
			
			app.UseIdentityServer();
			app.UseAuthorization();
			app.MapRazorPages().RequireAuthorization();
			return app;
		}


        public static void AddAAD(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddOpenIdConnect("AAD", "Login with Microsoft", options =>
                {
                    options.Authority = "https://login.microsoftonline.com/common";
                    options.TokenValidationParameters = new TokenValidationParameters { ValidateIssuer = false };
                    options.ClientId = "4de9ae1b-3487-4df1-abe0-782849f07985";
                    options.ClientSecret = "Ww2_@Wz/]Vt0KZmSTSbLdzfbR3pU2]sH";
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.CallbackPath = "/signin-aad";
                    options.RemoteSignOutPath = "/signout-aad";
                    options.SignedOutCallbackPath = "/signout-callback-aad";
                    //options.Prompt = "admin_consent";
                    options.Events = new OpenIdConnectEvents
                    {
                        OnAccessDenied = (context) =>
                        {
                            //context.Response.Redirect(ConfigManager.AppUrls.GetAADAccessDeniedRedirectUrl(context));
                            context.HandleResponse();
                            return Task.FromResult(0);
                        }
                    };
                });
        }

        public static void AddGoogle(this IServiceCollection services)
		{
			services.AddAuthentication()
				.AddGoogle(options =>
				{
					options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

					// register your IdentityServer with Google at https://console.developers.google.com
					// enable the Google+ API
					// set the redirect URI to https://localhost:5001/signin-google
					options.ClientId = "copy client ID from Google here";
					options.ClientSecret = "copy client secret from Google here";
				});
		}
	}
}