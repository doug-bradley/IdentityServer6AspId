using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using IdentityServer6AspId.Data;
using IdentityServer6AspId.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer6AspId.Services
{
	public class CustomProfileService : IProfileService
	{
		//private readonly IUserService _userService;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;
        private readonly ILogger<CustomProfileService> _logger;
		
		public CustomProfileService(UserManager<ApplicationUser> userMgr, ILogger<CustomProfileService> logger, RoleManager<IdentityRole> roleMgr,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
		{
			//_userService = userService;
			_logger = logger;

            _userMgr = userMgr;
            _roleMgr = roleMgr;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _logger.LogInformation("CustomProfile");
		}
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userMgr.FindByIdAsync(sub);
            var userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

            var claims = userClaims.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            if (_userMgr.SupportsUserRole)
            {
                var roles = await _userMgr.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, roleName));
                    if (_roleMgr.SupportsRoleClaims)
                    {
                        var role = await _roleMgr.FindByNameAsync(roleName);
                        if (role != null)
                        {
                            claims.AddRange(await _roleMgr.GetClaimsAsync(role));
                        }
                    }
                }
            }
            try
            {
                var cl = context.Subject.Claims.FirstOrDefault(i => i.Type == "tenant_id");

                
                if (cl != null)
                {
                    claims.Add(new Claim("tenant_id", cl.Value));
                }
                //claims.AddRange(userRoles.Select(r => new Claim(JwtClaimTypes.Role, r)));
                //claims.AddRange(userPermissions.Select(p => new Claim("permission", p)));

                context.AddRequestedClaims(claims);

            }
            catch (Exception ex)
            {
                var test = ex;

            }
            context.IssuedClaims = claims;
        }
        //public async Task GetProfileDataAsync2(ProfileDataRequestContext context)
        //{
        //    //var userId = context.Subject.FindFirstValue(JwtClaimTypes.Subject);
        //    //var user = await _dbContext.Users.Include(user => user.Roles).FirstOrDefaultAsync(tenantUser => tenantUser.UserId == userId);

        //    //if (!string.IsNullOrWhiteSpace(user?.UserName))
        //    //{
        //    //    context.IssuedClaims.Add(new Claim("name", user.UserName));
        //    //}
        //    //var userRoles = user.Roles;
        //    //foreach (var role in userRoles)
        //    //{
        //    //    context.IssuedClaims.Add(new Claim("role", role.Name));
        //    //}


        //    Console.WriteLine("GetProfileDataAsync called");
        //    // Assume the subject claim is your user identifier
        //    var userId = context.Subject.GetSubjectId();

        //    // Get your user data however you've stored it
        //    var user = await _userService.GetUserByIdAsync(userId);

        //    var userRoles = await _userService.GetUserRoles(user);
        //    userRoles.Add("ProcurementManager");
        //    userRoles.Add("Approver");

        //    const string userDepartment = "Procurement";

        //    var userPermissions = new List<string>() { "create-order", "approve-order", "view-all-orders" };

        //    try
        //    {
        //        var cl = context.Subject.Claims.FirstOrDefault(i => i.Type == "tenant_id");

        //        // Create a list of claims for the identity token and/or access token
        //        var claims = new List<Claim>
        //              {

        //                  new("currency", "USD"),
        //                  new("timezone", "PST"),
        //                  new("department", userDepartment)
        //              };
        //        if (cl != null)
        //        {
        //            claims.Add(new Claim("tenant_id", cl.Value));
        //        }
        //        claims.AddRange(userRoles.Select(r => new Claim(JwtClaimTypes.Role, r)));
        //        claims.AddRange(userPermissions.Select(p => new Claim("permission", p)));

        //        context.AddRequestedClaims(claims);

        //    }
        //    catch (Exception ex)
        //    {
        //        var test = ex;

        //    }

        //}

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userMgr.FindByIdAsync(sub);
            context.IsActive = user != null;
        }

    }

}
