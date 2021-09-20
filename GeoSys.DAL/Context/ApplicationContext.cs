#region - Using

using GeoSys.DAL.Mappings;
using GeoSys.DAL.Models;
using Microsoft.EntityFrameworkCore;

#endregion

namespace GeoSys.DAL.Context
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Server=.; Database=GeoSys; User ID=sa; Password=123;trusted_connection=true;");
        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductsGallery> ProductsGallery { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Veritabanındaki kolonların özelliklerini eklemek için Maplerimi OnModelCreating Metotdumun içerisine yazıyorum.

            modelBuilder.ApplyConfiguration(new CategoriesMap());
            modelBuilder.ApplyConfiguration(new ProductsMap());
            modelBuilder.ApplyConfiguration(new ProductsGalleryMap());

            modelBuilder.Entity<Categories>()
                   .HasOne(c => c.TopCategori)
                   .WithMany()
                   .HasForeignKey(c => c.TopCategoriId);

            modelBuilder.Entity<Products>()
                .HasMany(k => k.ProductsGallery)
                .WithOne(m => m.Products)
                .HasForeignKey(m => m.ProductId);
        }
    }
}
