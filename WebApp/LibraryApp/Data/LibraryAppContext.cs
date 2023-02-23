using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Models;

namespace LibraryApp.Data
{
    public class LibraryAppContext : DbContext
    {
        public LibraryAppContext (DbContextOptions<LibraryAppContext> options)
            : base(options)
        {
        }

        public DbSet<LibraryApp.Models.Book> Book { get; set; } = default!;
    }
}
