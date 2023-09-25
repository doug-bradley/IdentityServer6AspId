using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer6AspId.Pages.Device
{
	[SecurityHeaders]
	[Authorize]
	public class SuccessModel : PageModel
	{
		public void OnGet()
		{
		}
	}
}