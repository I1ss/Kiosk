namespace Kiosk.Order.Tests.Common
{
    using Kiosk.OrdersWebApi;
    using Kiosk.OrdersWebApi.Models;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Класс для создания контекста базы данных для тестов.
    /// </summary>
    public class OrderContextFactory
    {
        /// <summary>
        /// Константа идентификатора первого заказа.
        /// </summary>
        public const int FIRST_ORDER_ID = 1;

        /// <summary>
        /// Константа идентификатора второго заказа.
        /// </summary>
        public const int SECOND_ORDER_ID = 2;

        /// <summary>
        /// Константа идентификатора первого продукта в заказе.
        /// </summary>
        public const int FIRST_PRODUCT_ID = 1;

        /// <summary>
        /// Константа идентификатора второго продукта в заказе.
        /// </summary>
        public const int SECOND_PRODUCT_ID = 2;

        /// <summary>
        /// Константа идентификатора третьего продукта в заказе.
        /// </summary>
        public const int THIRD_PRODUCT_ID = 3;

        /// <summary>
        /// Константа идентификатора четвертого продукта в заказе.
        /// </summary>
        public const int FOURTH_PRODUCT_ID = 4;

        /// <summary>
        /// Создание контекста базы данных.
        /// </summary>
        /// <returns> Контекст БД для заказов. </returns>
        public static OrderDbContext Create()
        {
            var options = new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new OrderDbContext(options);
            context.Database.EnsureCreated();

            context.Orders.AddRange(
                new Order
                {
                    OrderId = FIRST_ORDER_ID
                },
                new Order
                {
                    OrderId = SECOND_ORDER_ID
                }
            );

            context.ProductsInOrder.AddRange(
                new ProductInOrder
                {
                    OrderId = FIRST_ORDER_ID,
                    ProductCode = "123",
                    ProductId = FIRST_PRODUCT_ID,
                    ProductName = "Каша овсяная",
                    ProductPrice = 75
                },
                new ProductInOrder
                {
                    OrderId = FIRST_ORDER_ID,
                    ProductCode = "234",
                    ProductId = SECOND_PRODUCT_ID,
                    ProductName = "Спортивные шорты",
                    ProductPrice = 1000
                },
                new ProductInOrder
                {
                    OrderId = SECOND_ORDER_ID,
                    ProductCode = "345",
                    ProductId = THIRD_PRODUCT_ID,
                    ProductName = "Кроссовки",
                    ProductPrice = 3500
                },
                new ProductInOrder
                {
                    OrderId = SECOND_ORDER_ID,
                    ProductCode = "456",
                    ProductId = FOURTH_PRODUCT_ID,
                    ProductName = "Говядина",
                    ProductPrice = 500
                }
            );

            context.SaveChanges();
            return context;
        }

        /// <summary>
        /// Удаление контекста базы данных.
        /// </summary>
        /// <param name="context"> Контекст БД для заказов. </param>
        public static void Destroy(OrderDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
