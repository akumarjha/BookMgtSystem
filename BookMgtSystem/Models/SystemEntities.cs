using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookMgtSystem.Models
{
    public class SystemEntities : DbContext
    {
        public SystemEntities()
            : base("name = BookMgtSystem")
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBook> AuthorBook { get; set; }
    }
}