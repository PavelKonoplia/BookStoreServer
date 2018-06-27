using BookStore.BLL;
using BookStore.Entity.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookStore.Controllers
{
    public class UserController : ApiController
    {
        private IdentityUserManager userManager;

        public UserController(IdentityUserManager _userManager)
        {
            userManager = _userManager;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/user")]
        public IHttpActionResult Get()
        {
            return Ok(this.userManager.Users);
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
        public async Task<IHttpActionResult> Update(long id, string oldRole, string newRole)
        {
            User user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                await userManager.RemoveFromRoleAsync(user.Id, oldRole);
                await userManager.AddToRoleAsync(user.Id, newRole);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // TODO RoleRequest 

        //[Authorize(Roles = "Admin")]
        //[HttpPut]
        //[Route("api/user")]
        //public async Task<IHttpActionResult> Update(long id, string oldRole, string newRole)
        //{
        //    User user = await userManager.FindByIdAsync(id);

        //    if (user != null)
        //    {
        //        await userManager.RemoveFromRoleAsync(user.Id, oldRole);
        //        await userManager.AddToRoleAsync(user.Id, newRole);
        //        return Ok();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
    }
}
