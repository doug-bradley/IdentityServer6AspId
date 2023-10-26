using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

namespace IdentityServer6AspId.Services
{
	public class CustomProfileService : IProfileService
	{

		public Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			throw new NotImplementedException();
		}

		public Task IsActiveAsync(IsActiveContext context)
		{
			throw new NotImplementedException();
		}
		//private readonly IUserService _userService;
		//private readonly ILogger<CustomProfileService> _logger;

		//public CustomProfileService(IUserService userService, ILogger<CustomProfileService> logger)
		//{
		//	_userService = userService;
		//	_logger = logger;

		//	Console.WriteLine("GetProfileDataAsync called");
		//	_logger.LogInformation("CustomProfile");
		//}

		//public Task GetProfileDataAsync(ProfileDataRequestContext context)
		//{

		//	Console.WriteLine("GetProfileDataAsync called");
		//	// Assume the subject claim is your user identifier
		//	var userId = context.Subject.GetSubjectId();

		//	// Get your user data however you've stored it
		//	//var user = await _userService.GetUserByIdAsync(userId);

		//	// Create a list of claims for the identity token and/or access token
		//	var claims = new List<Claim>
		//	{
		//		new Claim("tenant_id", "234"),
		//		new Claim("currency", "USD"),
		//		new Claim("timezone", "PST")
		//		//new Claim("tenant_id", user.TenantId),
		//		//new Claim("currency", user.Currency),
		//		//new Claim("timezone", user.TimeZone),
		//	};

		//	context.IssuedClaims.AddRange(claims);
		//	return Task.CompletedTask;
		//}

		//public Task IsActiveAsync(IsActiveContext context)
		//{
		//	context.IsActive = true;
		//	return Task.CompletedTask;

		//	//_logger.LogInformation("CustomProfile");

		//	var userId = context.Subject.GetSubjectId();
		//	//var user = await _userService.GetUserByIdAsync(userId);

		//	//context.IsActive = user != null;
		//}

	}

}
