using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using BookStore.Entity.Models;

namespace BookStore.DAL
{
    public class CustomUserStore : UserStore<User, CustomRole, long,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(DbContext context)
            : base(context)
        {
        }
    }
}
