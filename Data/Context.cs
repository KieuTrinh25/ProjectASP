using Microsoft.EntityFrameworkCore;
using ProjectASP.Models;
using System.Linq.Dynamic.Core;

namespace ProjectASP.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options){ }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<User> users { get; set; }
        public (Product[] products, int pages, int page) Paging(int page, string? search, int? id)
        {
            int size = 3;
            var product = products.Include(p => p.Category).ToArray();
            if (id != 0)
            {
                product = product.Where(x => x.categoryId == id).ToArray();
            }
            if (!String.IsNullOrEmpty(search))
            {
                product = product.Where(p => p.name.ToLower().Contains(search.ToLower())).ToArray();
            }
            int pages = (int)Math.Ceiling((double)product.Count() / size);
            product = product.Skip((page - 1) * size).Take(size).ToArray();
            return (product, pages, page);
        }
    }
}
