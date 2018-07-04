using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Entity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
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
            if (this.userManager.GetRoles(this.user.Id).Count != 0)
            {
                context.AdditionalResponseParameters.Add("role", this.userManager.GetRoles(this.user.Id)[0]);
            }


            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            this.user = await userManager.FindAsync(context.UserName, context.Password);

            if (this.user == null)
            {
                context.SetError("invalid_grant",
                "User name or password is incorrect.");
                return;
            }

            //context.Validated(new ClaimsIdentity(user as IEnumerable<Claim>, context.Options.AuthenticationType));
            
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));

            //this loop is where the roles are added as claims
            foreach (var role in userManager.GetRoles(user.Id))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                "as:client_id", context.ClientId ?? string.Empty
                },
                {
                "userName", context.UserName
                }
            });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }
    }
}
