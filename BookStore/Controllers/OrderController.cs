using BookStore.Common.Interfaces;
using BookStore.Entity.Models;
using System.Web.Http;

namespace BookStore.Controllers
{
    [Authorize]
    public class OrderController : ApiController
    {
        IRepository<Order> dataProvider;

        public OrderController(IRepository<Order> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        [HttpGet]
        [Route("api/order")]
        public IHttpActionResult Get()
        {
            return Ok(dataProvider.GetAll());
        }
        
        [HttpPost]
        [Route("api/order")]
        public IHttpActionResult Post([FromBody]Order order)
        {
            dataProvider.Add(order);
            return Ok();
        }
    }
}
