using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Entity.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }

        public decimal Price { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
