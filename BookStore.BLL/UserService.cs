using BookStore.DAL;
using BookStore.Entity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.BLL
{
    public class UserService
    {
        private IdentityUserManager userManager;

        private IQueryable<CustomRole> roles;

        public UserService(IdentityUserManager userManager, IdentityDatabaseContext dbContext)
        {
            this.userManager = userManager;

            this.roles = dbContext.Roles.Distinct() as IQueryable<CustomRole>;
        }

        public IEnumerable<UserInfo> GetUsers()
        {            
            var users = userManager.Users.Select(user => new UserInfo
            {
                UserName = user.UserName,
                Id = user.Id,
                Email = user.Email,
                RoleName = roles.FirstOrDefault(r=> r.Id == user.Roles.FirstOrDefault().RoleId).Name
            }).ToArray() ;

            return users;
        }

        private string GetRoleName(long id)
        {
            return this.roles.First(r => r.Id == id).Name;
        }
    }
}
