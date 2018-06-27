using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Entity.Models
{
    public class Order
    {
        public Order()
        {
            this.Goods = new HashSet<BaseModel>();
        }

        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        public ICollection<BaseModel> Goods { get; set; }
    }
}
