namespace Kiosk.CatalogWebApi
{
    using Microsoft.EntityFrameworkCore;

    using Kiosk.CatalogWebApi.Models;

    /// <summary>
    /// Контекст БД для каталога.
    /// </summary>
    public class CatalogDbContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста БД для каталога.
        /// </summary>
        public CatalogDbContext(DbContextOptions<CatalogDbContext> dbsOptions) : base(dbsOptions) { }

        /// <summary>
        /// Категории.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Бренды.
        /// </summary>
        public DbSet<Brand> Brands { get; set; }

        /// <summary>
        /// Продукты.
        /// </summary>
        public DbSet<Product> Products { get; set; }
    }
}
