using BookStore.BLL;
using BookStore.DAL;
using BookStore.Entity.Enums;
using BookStore.Entity.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookStore.Controllers
{
    public class UserController : ApiController
    {
        private IdentityUserManager userManager;
        private UserService userService;

        public UserController(IdentityUserManager userManager, UserService userService)
        {
            this.userService = userService;
            this.userManager = userManager;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/user")]
        public IHttpActionResult Get()
        {
          //  IQueryable<User> query = this.userManager.Users;
           // query = query.Include(x => x.Selling).Include(x => x.Orders);

            return Ok(this.userService.GetUsers());
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/user/{login}")]
        public async Task<IHttpActionResult> Get(string login)
        {
            var user = await this.userManager.FindByNameAsync(login);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("api/user")]
        public async Task<IHttpActionResult> Post([FromBody]User user)
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
                return StatusCode(System.Net.HttpStatusCode.BadRequest);
            }

            await this.userManager.AddToRoleAsync(userData.Id, Role.Customer.ToString());

            return Created("api/user", user);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("api/user/{id}")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            User user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("api/user")]
        public async Task<IHttpActionResult> UpdateRole([FromUri] long id, [FromUri]string role)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                await userManager.RemoveFromRolesAsync(user.Id, Enum.GetNames(typeof(Role)));
                await userManager.AddToRoleAsync(user.Id, role);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


        [Authorize]
        [HttpGet]
        [Route("api/user")]
        public async Task<IHttpActionResult> GetRole([FromUri]long id)
        {
            User user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                var roles = await userManager.GetRolesAsync(user.Id);
                return Ok(roles);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
