using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Entity.Models
{
    public class BaseProductModel
    {
        public BaseProductModel()
        {
            this.Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }

        public decimal Price { get; set; }

        public long? UserId { get; set; }

        public User User { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
