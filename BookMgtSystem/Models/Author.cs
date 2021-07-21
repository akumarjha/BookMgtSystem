using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookMgtSystem.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<AuthorBook> AuthorsBooks { get; set; }
    }
}