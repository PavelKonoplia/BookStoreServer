using BookStore.DAL;
using BookStore.Entity.Enums;
using BookStore.Entity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

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
                RoleName = roles.FirstOrDefault(r => r.Id == user.Roles.FirstOrDefault().RoleId).Name
            }).ToArray();

            return users;
        }

        public UserInfo FindByName(string name)
        {
            User user = this.userManager.FindByNameAsync(name).Result;

            return new UserInfo
            {
                UserName = user.UserName,
                Id = user.Id,
                Email = user.Email,
                RoleName = roles.FirstOrDefault(r => r.Id == user.Roles.FirstOrDefault().RoleId).Name
            };
        }

        public async Task<bool> AddUser(User user)
        {
            var userData = new User()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email == null ? $"{user.UserName} {user.Id}@gmail.com" : user.Email
            };

            IdentityResult addUserResult = await this.userManager.CreateAsync(userData, user.PasswordHash);

            if (!addUserResult.Succeeded)
            {
                return false;
            }

            await this.userManager.AddToRoleAsync(userData.Id, Role.Customer.ToString());
            
            return true;
        }

        public async Task<bool> UpdateRole(long id, string role)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var roles = Enum.GetNames(typeof(Role));
                foreach (var r in roles)
                {
                    await this.userManager.RemoveFromRoleAsync(user.Id, r);
                }
                
                await this.userManager.AddToRoleAsync(user.Id, role);

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(long id)
        {
            User user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
