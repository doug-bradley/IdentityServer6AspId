using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;

namespace IdentityServer6AspId.Services
{
	public class CustomProfileService : IProfileService
	{
		private readonly IUserService _userService;
		private readonly ILogger<CustomProfileService> _logger;

		public CustomProfileService(IUserService userService, ILogger<CustomProfileService> logger)
		{
			_userService = userService;
			_logger = logger;

			Console.WriteLine("GetProfileDataAsync called");
			_logger.LogInformation("CustomProfile");
		}

		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{

			Console.WriteLine("GetProfileDataAsync called");
			// Assume the subject claim is your user identifier
			var userId = context.Subject.GetSubjectId();

			// Get your user data however you've stored it
			var user = await _userService.GetUserByIdAsync(userId);
            const string userDepartment = "Procurement";
			var userRoles = new List<string>() { "ProcurementManager", "Approver"};
			var userPermissions = new List<string>() { "create-order", "approve-order", "view-all-orders"};

            var cl = context.Subject.Claims.First(i => i.Type == "tenant_id");
			
			// Create a list of claims for the identity token and/or access token
			var claims = new List<Claim>
			{
				new("tenant_id", cl.Value),
				new ("currency", "USD"),
				new ("timezone", "PST"),
                new ("department", userDepartment)
			};

            claims.AddRange(userRoles.Select(r => new Claim(JwtClaimTypes.Role, r)));
            claims.AddRange(userPermissions.Select(p => new Claim("permission", p)));

            context.AddRequestedClaims(claims);

        }

		public Task IsActiveAsync(IsActiveContext context)
		{
			context.IsActive = true;
			return Task.CompletedTask;

			//_logger.LogInformation("CustomProfile");

			var userId = context.Subject.GetSubjectId();
			//var user = await _userService.GetUserByIdAsync(userId);

			//context.IsActive = user != null;
		}

	}

}
