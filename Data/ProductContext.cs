using System; 
using Microsoft.EntityFrameworkCore; 

namespace WishList 
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        public DbSet<ProductMeta> ProductMeta {get; set;}
        public DbSet<Product> Product {get; set;}

    }
}