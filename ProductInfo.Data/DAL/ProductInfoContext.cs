using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProductInfo.Utils;

namespace ProductInfo.Data.Entities
{
    public partial class ProductInfoContext : DbContext
    {
        public ProductInfoContext()
        {
        }

        public ProductInfoContext(DbContextOptions<ProductInfoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Price> Price { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Constants.DBConnectionString);
            }
        }
    }
}
