using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Authentication;

namespace IdentityServer6AspId.Services
{
    public class TenantInteractionResponseGenerator : AuthorizeInteractionResponseGenerator
    {
        public TenantInteractionResponseGenerator(IdentityServerOptions options, ISystemClock clock, ILogger<AuthorizeInteractionResponseGenerator> logger, IConsentService consent, IProfileService profile) : base(options, clock, logger, consent, profile)
        {
        }
        
        // Example implementation of ProcessInteractionAsync
        public override async Task<InteractionResponse> ProcessInteractionAsync(ValidatedAuthorizeRequest request, ConsentResponse consent = null)
        {
            
            var response = await base.ProcessInteractionAsync(request, consent);
            
            if (response.IsConsent || response.IsLogin || response.IsError)
                return response;

            if (!request?.Subject?.HasClaim(c => c.Type == "tenant_id" ) ?? false)
                return new InteractionResponse
                {
                    RedirectUrl = "/Account/Tenant"
                };

            return new InteractionResponse();
        }
        
        

    }
}
