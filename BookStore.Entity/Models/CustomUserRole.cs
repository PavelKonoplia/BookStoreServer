using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Entity.Models
{
    public class CustomUserRole : IdentityUserRole<long>
    {
        [Key]
        public long Id { get; set; }
    }
}
