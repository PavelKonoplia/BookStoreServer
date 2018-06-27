using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Entity.Models
{
    public class CustomUserLogin : IdentityUserLogin<long>
    {
        [Key]
        public long Id { get; set; }
    }
}
