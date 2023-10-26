using System.Security.Claims;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using IdentityModel;
using IdentityServer6AspId.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;
using ApiResource = Duende.IdentityServer.Models.ApiResource;
using ApiScope = Duende.IdentityServer.Models.ApiScope;
using Client = Duende.IdentityServer.Models.Client;
using ClientClaim = Duende.IdentityServer.Models.ClientClaim;
using IdentityResource = Duende.IdentityServer.Models.IdentityResource;
using Secret = Duende.IdentityServer.Models.Secret;

namespace IdentityServer6AspId.Data
{
	public static class SeedData
	{

		public static void SeedUsers(WebApplication app)
		{
			using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			var alice = userMgr.FindByNameAsync("alice").Result;
			if (alice == null)
			{
				alice = new ApplicationUser
				{
					UserName = "alice",
					Email = "AliceSmith@email.com",
					EmailConfirmed = true,
				};
				var result = userMgr.CreateAsync(alice, "Pass123$").Result;
				if (!result.Succeeded)
				{
					throw new Exception(result.Errors.First().Description);
				}

				result = userMgr.AddClaimsAsync(alice, new Claim[]
				{
					new Claim(JwtClaimTypes.Name, "Alice Smith"),
					new Claim(JwtClaimTypes.GivenName, "Alice"),
					new Claim(JwtClaimTypes.FamilyName, "Smith"),
					new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
				}).Result;
				if (!result.Succeeded)
				{
					throw new Exception(result.Errors.First().Description);
				}

				Log.Debug("alice created");
			}
			else
			{
				Log.Debug("alice already exists");
			}

			var bob = userMgr.FindByNameAsync("bob").Result;
			if (bob == null)
			{
				bob = new ApplicationUser
				{
					UserName = "bob",
					Email = "BobSmith@email.com",
					EmailConfirmed = true,
					TenantId = Guid.NewGuid().ToString(),
					Currency = "USD",
					TimeZone = "Pacific Standard Time"
				};
				var result = userMgr.CreateAsync(bob, "Pass123$").Result;
				if (!result.Succeeded)
				{
					throw new Exception(result.Errors.First().Description);
				}

				result = userMgr.AddClaimsAsync(bob, new Claim[]
				{
					new Claim(JwtClaimTypes.Name, "Bob Smith"),
					new Claim(JwtClaimTypes.GivenName, "Bob"),
					new Claim(JwtClaimTypes.FamilyName, "Smith"),
					new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
					new Claim("location", "somewhere")
				}).Result;
				if (!result.Succeeded)
				{
					throw new Exception(result.Errors.First().Description);
				}

				Log.Debug("bob created");
			}
			else
			{
				Log.Debug("bob already exists");
			}
		}

		public static void SeedClients(WebApplication app)
		{
			using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

			const string clientId = "multiTenantClient";

			var clientExists = context.Clients.Any(i => i.ClientId == clientId);

			if (clientExists)
			{
				Log.Information("Client already exists");
				return;
			}

			var newClient = new Client
			{
				ClientId = clientId,
				ClientName = "Multi-Tenant Procurement App",
				AllowedGrantTypes = GrantTypes.Code,
				RequirePkce = true,
				ClientClaimsPrefix = "tenant_",
				AllowedScopes = { "openid", "profile", "api1", "custom" },
				AllowedCorsOrigins = { "http://localhost:5173" },
				AccessTokenLifetime = 3600,
				RefreshTokenUsage = TokenUsage.OneTimeOnly,
				RedirectUris = { "http://localhost:5173/callback" },
				PostLogoutRedirectUris = { "http://localhost:5173/" },
				RequireClientSecret = false,
				RequireConsent = false,
				AllowOfflineAccess = true,
				AlwaysIncludeUserClaimsInIdToken = true,
				AlwaysSendClientClaims = true
			};

			context.Clients.Add(newClient.ToEntity());
			context.SaveChanges();
		}

		public static void SeedResources(WebApplication app)
		{
			using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
			const string resourceName = "procurement-api";
			var resourceExists = context.ApiResources.Any(i => i.Name == resourceName);
			if (resourceExists)
			{
				Log.Information("Resource already exists");
				return;
			}
			

			var apiResource = new ApiResource
			{
				Name = resourceName,
				DisplayName = "Procurement API",
				Description = "API for procurement operations",
				UserClaims = new List<string> { "role", "tenant_id", "name", "currency", "timezone" },
				Scopes = new List<string>
				{
					"procurement.read",
					"procurement.write"
				},
				ApiSecrets = new List<Secret> { new Secret("apiSecret".Sha256()) }
			};

			context.ApiResources.Add(apiResource.ToEntity());

			// Add the associated scopes
			var apiScopes = new List<ApiScope>
			{
				new ApiScope("procurement.read", "Read Access for Procurement API"),
				new ApiScope("procurement.write", "Write Access for Procurement API")
			};

			foreach (var apiScope in apiScopes.Where(apiScope => context.ApiScopes.Any(s => s.Name == apiScope.Name) == false))
			{
				context.ApiScopes.Add(apiScope.ToEntity());
			}

			context.SaveChanges();
		}

		public static void SeedIdentityResources(WebApplication app)
		{
			using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

			// Check if any IdentityResources already exist
			if (context.IdentityResources.Any())
			{
				return;
			}

			// Define the IdentityResources
			var identityResources = new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResource
				{
					Name = "custom",
					DisplayName = "Custom profile",
					UserClaims = new List<string>
					{
						"tenant_id",
						"currency",
						"timezone",
						"department"
					}
				}
				//new IdentityResources.Email(),
				//new IdentityResource("roles", new[] { "role" }),
				//new IdentityResource("tenant", new[] { "tenant_id" }),
			};
			

			// Add the IdentityResources to the DbContext
			foreach (var resource in identityResources)
			{
				context.IdentityResources.Add(resource.ToEntity());
			}

			// Save changes
			context.SaveChanges();
		}

	}
}