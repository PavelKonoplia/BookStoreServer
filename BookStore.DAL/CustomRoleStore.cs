using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using BookStore.Entity.Models;

namespace BookStore.DAL
{
    public class CustomRoleStore : RoleStore<CustomRole, long, CustomUserRole>
    {
        public CustomRoleStore(DbContext context)
            : base(context)
        {
        }
    }
}
