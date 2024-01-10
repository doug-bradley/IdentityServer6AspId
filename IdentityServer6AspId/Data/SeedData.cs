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
                var client = context.Clients.First(i => i.ClientId == clientId);
                context.Clients.Remove(client);
                context.SaveChanges();
				
                Log.Information("Client already exists");
				
			}

            var clientUri = "http://localhost:3000";
			
			
			var newClient = new Client
			{
				ClientUri = $"{clientUri}",
				ClientId = clientId,
				ClientName = "Multi-Tenant Procurement App",
				AllowedGrantTypes = GrantTypes.Code,
				RequirePkce = true,
				//ClientClaimsPrefix = "tenant_",
				AllowedScopes = { "openid", "profile", "email", "tenant_id", "role", "permission", "department" },
				AllowedCorsOrigins = { $"{clientUri}" },
				AccessTokenLifetime = 3600,
				RefreshTokenUsage = TokenUsage.OneTimeOnly,
				RedirectUris = { $"{clientUri}", $"{clientUri}/callback", $"{clientUri}/callback-popup", $"{clientUri}/silent-renew"},
				PostLogoutRedirectUris = { $"{clientUri}/signout", $"{clientUri}/signout-popup" },
				RequireClientSecret = false,
				RequireConsent = false,
				AllowOfflineAccess = true,
				AlwaysIncludeUserClaimsInIdToken = true,
				AlwaysSendClientClaims = true
			};
			

			//var newClient = new Client
			//{
			//	ClientUri = "http://localhost:3000",
			//	ClientId = clientId,
			//	ClientName = "Multi-Tenant Procurement App",
			//	AllowedGrantTypes = GrantTypes.Code,
			//	RequirePkce = true,
			//	//ClientClaimsPrefix = "tenant_",
			//	AllowedScopes = { "openid", "profile", "email", "tenant_id", "role", "permission", "department" },
			//	AllowedCorsOrigins = { "http://localhost:3000" },
			//	AccessTokenLifetime = 3600,
			//	RefreshTokenUsage = TokenUsage.OneTimeOnly,
			//	RedirectUris = { "http://localhost:3000/api/auth/callback/duende-identityserver6" },
			//	PostLogoutRedirectUris = { "http://localhost:3000/" },
			//	RequireClientSecret = false,
			//	RequireConsent = false,
			//	AllowOfflineAccess = true,
			//	AlwaysIncludeUserClaimsInIdToken = true,
			//	AlwaysSendClientClaims = true
			//};
			
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
                var resource = context.ApiResources.First(i => i.Name == resourceName);
				context.ApiResources.Remove(resource);
                context.SaveChanges();
				
				Log.Information("Resource already exists");
				
			}
			

			var apiResource = new ApiResource
			{
				Name = resourceName,
				DisplayName = "Procurement API",
				Description = "API for procurement operations",
				UserClaims = new List<string> { "tenant_id", "role", "permission", "department" },
				Scopes = new List<string>
				{
					"procurement.read",
					"procurement.write"
                },
				ApiSecrets = new List<Secret> { new("apiSecret".Sha256()) }
			};

			context.ApiResources.Add(apiResource.ToEntity());

			// Add the associated scopes
			var apiScopes = new List<ApiScope>
			{
				new("procurement.read", "Read Access for Procurement API"),
				new("procurement.write", "Write Access for Procurement API"),
                new("offline_access")
                , new("email")
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
				context.IdentityResources.RemoveRange(context.IdentityResources);
                context.SaveChanges();
			}

			// Define the IdentityResources
			var identityResources = new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
                new("tenant_id", new [] { "tenant_id" }),
                new("role", new [] { "role" }),
                new("permission", new [] { "permission" }),
                new("department", new [] { "department" })
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