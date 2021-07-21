using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMgtSystem.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<AuthorBook> AuthorsBooks { get; set; }
    }
}