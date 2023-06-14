namespace Kiosk.DeliveryWebApi
{
    using Kiosk.DeliveryWebApi.Models;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Контекст БД для доставки.
    /// </summary>
    public class DeliveryDbContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста БД для доставки.
        /// </summary>
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> dbsOptions) : base(dbsOptions) { }

        /// <summary>
        /// Задания.
        /// </summary>
        public DbSet<Issue> Issues { get; set; }
    }
}
