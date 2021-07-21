using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMgtSystem.Models
{
    public class AuthorsViewModel
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public List<CheckBoxViewModel> Books { get; set; }
    }
}