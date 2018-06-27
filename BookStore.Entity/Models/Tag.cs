using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Entity.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Books = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string TagName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
