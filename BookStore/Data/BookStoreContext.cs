using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {

        }

        public DbSet<Books> Books { get; set; }
        public DbSet<BookGallery> BookGallery { get; set; }
        public DbSet<Language> Language { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        //{
        //    optionbuilder.UseSqlServer("Server=JANVI-DESAI\\SQLEXPRESS; Database=BookStore; Integrated Security=True");
        //    base.OnConfiguring(optionbuilder);
        //} It is not needed for declare here, bcoz we define in startup.cs file ;)
    }
}
