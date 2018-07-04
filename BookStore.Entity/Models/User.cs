using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookStore.Entity.Models
{
    public class User : IdentityUser<long, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {

        public User()
        {
            this.Selling = new HashSet<BaseProductModel>();

            this.Orders = new HashSet<Order>();
        }

        public ICollection<BaseProductModel> Selling { get; set; }

        public ICollection<Order> Orders { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, long> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(
                this, DefaultAuthenticationTypes.ExternalBearer);

            return userIdentity;
        }
    }
}
