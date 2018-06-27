using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Entity.Models
{
    public class Book : BaseModel
    {
        public Book()
        {
            this.Tags = new HashSet<Tag>();
        }

        public string Title { get; set; }

        [MinLength(8)]
        [MaxLength(8)]
        [RegularExpression(@"^[0-9]*$")]
        public string ISO { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

        public string Author { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
