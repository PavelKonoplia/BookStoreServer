using Microsoft.AspNet.Identity.EntityFramework;

namespace BookStore.Entity.Models
{
    public class CustomRole : IdentityRole<long, CustomUserRole>
    {
        public CustomRole()
        {
        }

        public CustomRole(string name)
        {
            Name = name;
        }
    }
}
