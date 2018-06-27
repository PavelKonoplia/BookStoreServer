using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Entity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;

namespace BookStore.BLL
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private IdentityUserManager userManager;
        private User user;

        public ApplicationOAuthProvider(IdentityUserManager userManager)
        {
            this.userManager = userManager;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            context.AdditionalResponseParameters.Add("userID", this.user.Id);

            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            this.user = await userManager.FindAsync(context.UserName, context.Password);

            if (this.user == null)
            {
                context.SetError("invalid_grant",
                "The user name or password is incorrect.");
                return;
            }

            context.Validated(new ClaimsIdentity(context.Options.AuthenticationType));
        }
    }
}
