using System.Security.Claims;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using IdentityServer6AspId.Data;
using IdentityServer6AspId.Models;
using IdentityServer6AspId.Pages.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentityServer6AspId.Pages.Account.Tenant
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IIdentityProviderStore _identityProviderStore;

        public ViewModel View { get; set; }

        [BindProperty]
        public string SelectedItemId { get; set; }
        
        [BindProperty]
        public IEnumerable<SelectListItem> Tenants { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public IndexModel(ApplicationDbContext context,
            IIdentityServerInteractionService interaction,
            IAuthenticationSchemeProvider schemeProvider,
            IIdentityProviderStore identityProviderStore,
            IEventService events,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _schemeProvider = schemeProvider;
            _identityProviderStore = identityProviderStore;
            _events = events;
        }

        public async Task<IActionResult> OnGet(string returnUrl)
        {
            await BuildModelAsync(returnUrl);
            
            var subjectId = _signInManager.Context.User.Identity.GetSubjectId();
            
            var tenantIds = _context.AspNetTenantUser.Where(i => i.LoginUserId == subjectId).Select(i => i.FxTenantId);
            var temp = _context.AspNetTenant.Where(i => tenantIds.Contains(i.FxTenantId));
            
            Tenants = temp.Select(i => new SelectListItem() { Value = i.FxTenantId.ToString(), Text = $"{i.Code}-{i.Name}" });
            
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var additionalLocalClaims = new List<Claim>();
            additionalLocalClaims.Add(new Claim("tenant_id", SelectedItemId));
            var user1 = await _userManager.GetUserAsync(_signInManager.Context.User);
            
            await _signInManager.SignInWithClaimsAsync(user1, true, additionalLocalClaims);

            return Redirect(Input.ReturnUrl);
        }

        private async Task BuildModelAsync(string returnUrl)
        {
            Input = new InputModel
            {
                ReturnUrl = returnUrl
            };

            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == Duende.IdentityServer.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                View = new ViewModel
                {
                    EnableLocalLogin = local,
                };

                Input.Username = context?.LoginHint;

                if (!local)
                {
                    View.ExternalProviders = new[] { new ViewModel.ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return;
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null)
                .Select(x => new ViewModel.ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name
                }).ToList();

            var dyanmicSchemes = (await _identityProviderStore.GetAllSchemeNamesAsync())
                .Where(x => x.Enabled)
                .Select(x => new ViewModel.ExternalProvider
                {
                    AuthenticationScheme = x.Scheme,
                    DisplayName = x.DisplayName
                });
            providers.AddRange(dyanmicSchemes);


            var allowLocal = true;
            var client = context?.Client;
            if (client != null)
            {
                allowLocal = client.EnableLocalLogin;
                if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                {
                    providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                }
            }

            View = new ViewModel
            {
                AllowRememberLogin = LoginOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && LoginOptions.AllowLocalLogin,
                ExternalProviders = providers.ToArray()
            };
        }
    }
}
