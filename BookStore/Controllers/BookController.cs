using BookStore.Common.Interfaces;
using BookStore.Entity.Models;
using System.Web.Http;
using System.Data.Entity;
using System.Linq;

namespace BookStore.Controllers
{
    public class BookController : ApiController
    {
        IRepository<Book> dataProvider;

        public BookController(IRepository<Book> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        [HttpGet]
        [Route("api/book")]
        public IHttpActionResult Get()
        {
            IQueryable<Book> query = dataProvider.GetAll() as IQueryable<Book>;            
            query = query.Include(x => x.Tags);

            return Ok(query.ToArray());
        }

        [Authorize]
        [Route("api/book/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(dataProvider.FindBy(d => d.Id == id));
        }
        
        [Authorize(Roles = "Seller")]
        [HttpPost]
        [Route("api/book")]
        public IHttpActionResult Post([FromBody]Book book)
        {
            dataProvider.Add(book);
            return Ok();
        }
        
        [Authorize(Roles = "Seller, Admin")]
        [Route("api/book/{id}")]
        public IHttpActionResult Delete(int id)
        {
            dataProvider.Delete(id);
            return Ok();
        }

        [Authorize(Roles = "Seller")]
        [Route("api/book")]
        public IHttpActionResult Put([FromBody]Book book)
        {
            dataProvider.Update(book);
            return Ok();
        }
    }
}
