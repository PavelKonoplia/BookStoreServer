using BookStore.BLL;
using BookStore.Entity.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookStore.Controllers
{
    public class UserController : ApiController
    {
        private UserService userService;

        public UserController( UserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/user")]
        public IHttpActionResult Get()
        {
            return Ok(this.userService.GetUsers());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/user/{login}")]
        public async Task<IHttpActionResult> Get(string login)
        {
            var result = this.userService.FindByName(login);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("api/user")]
        public async Task<IHttpActionResult> Post([FromBody]User user)
        {
            var result = await this.userService.AddUser(user);

            if (result)
            {
                return Ok(user);
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("api/user/{id}")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var result = await this.userService.DeleteUser(id);

            if (result)
            {
                return Ok();
            }
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("api/user")]
        public async Task<IHttpActionResult> UpdateRole([FromUri] long id, [FromUri]string role)
        {

            var result = await userService.UpdateRole(id, role);

            if (result)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
