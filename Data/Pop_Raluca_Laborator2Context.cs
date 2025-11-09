using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pop_Raluca_Laborator2.Models;

namespace Pop_Raluca_Laborator2.Data
{
    public class Pop_Raluca_Laborator2Context : DbContext
    {
        public Pop_Raluca_Laborator2Context (DbContextOptions<Pop_Raluca_Laborator2Context> options)
            : base(options)
        {
        }

        public DbSet<Pop_Raluca_Laborator2.Models.Book> Book { get; set; } = default!;
        public DbSet<Pop_Raluca_Laborator2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Pop_Raluca_Laborator2.Models.Author> Author { get; set; } = default!;
        public DbSet<Pop_Raluca_Laborator2.Models.Category> Category { get; set; } = default!;
        public DbSet<Pop_Raluca_Laborator2.Models.Member> Member { get; set; } = default!;
        public DbSet<Pop_Raluca_Laborator2.Models.Borrowing> Borrowing { get; set; } = default!;

    }
}
