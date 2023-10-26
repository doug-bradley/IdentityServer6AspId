// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Identity;

namespace IdentityServer6AspId.Models
{
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : IdentityUser
	{
		public string TenantId { get; set; }
		public string Currency { get; set; }
		public string TimeZone { get; set; }
	}
}