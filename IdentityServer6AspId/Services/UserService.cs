using IdentityServer6AspId.Data;
using IdentityServer6AspId.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer6AspId.Services
{
	public interface IUserService
	{
		Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<IList<string>> GetUserRoles(ApplicationUser user);
    }

	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public UserService(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
		{
			_userManager = userManager;
            _dbContext = applicationDbContext;
        }
		
		public async Task<ApplicationUser> GetUserByIdAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);

            return user;

        }
		
		public async Task<IList<string>> GetUserRoles(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
