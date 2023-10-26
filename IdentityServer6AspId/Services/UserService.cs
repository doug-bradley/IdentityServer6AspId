using IdentityServer6AspId.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer6AspId.Services
{
	public interface IUserService
	{
		Task<ApplicationUser> GetUserByIdAsync(string userId);
	}

	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		
		public async Task<ApplicationUser> GetUserByIdAsync(string userId)
		{
			return await _userManager.FindByIdAsync(userId);
		}
	}
}
