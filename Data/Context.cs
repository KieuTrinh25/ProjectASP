using Microsoft.EntityFrameworkCore;
using ProjectASP.Models; 

namespace ProjectASP.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<User> users { get; set; }

    }
}
