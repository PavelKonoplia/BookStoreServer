using System.ComponentModel.DataAnnotations;

namespace BookStore.Entity.Models
{
    public class CustomerToSellerRequest
    {
        [Key]
        public int Id { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
