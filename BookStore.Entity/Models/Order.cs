using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Entity.Models
{
    public class Order
    {
        public Order()
        {
            this.Goods = new HashSet<BaseProductModel>();
        }

        [Key]
        public int Id { get; set; }
        
        public long UserId { get; set; }
        public virtual User User { get; set; }

        public ICollection<BaseProductModel> Goods { get; set; }
    }
}
