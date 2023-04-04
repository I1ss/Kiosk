namespace Kiosk.OrdersWebApi
{
    using Kiosk.OrdersWebApi.Models;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Контекст БД для заказов.
    /// </summary>
    public class OrderDbContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста БД для заказов.
        /// </summary>
        public OrderDbContext(DbContextOptions<OrderDbContext> dbsOptions) : base(dbsOptions) { }

        /// <summary>
        /// Заказы.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Продукты в заказе.
        /// </summary>
        public DbSet<ProductInOrder> ProductsInOrder { get; set; }
    }
}
