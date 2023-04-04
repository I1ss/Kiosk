namespace Kiosk.Delivery.Tests.Common
{
    using Kiosk.DeliveryWebApi;
    using Kiosk.DeliveryWebApi.Models;

    using Microsoft.EntityFrameworkCore;
    
    /// <summary>
    /// Класс для создания контекста базы данных для тестов.
    /// </summary>
    public class DeliveryContextFactory
    {
        /// <summary>
        /// Константа идентификатора первого заказа.
        /// </summary>
        public const int FIRST_ISSUE_ID = 1;

        /// <summary>
        /// Константа идентификатора второго заказа.
        /// </summary>
        public const int SECOND_ISSUE_ID = 2;

        /// <summary>
        /// Создание контекста базы данных.
        /// </summary>
        /// <returns> Контекст БД для заказов. </returns>
        public static DeliveryDbContext Create()
        {
            var options = new DbContextOptionsBuilder<DeliveryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DeliveryDbContext(options);
            context.Database.EnsureCreated();

            context.Issues.AddRange(
                new Issue
                {
                    IssueId = FIRST_ISSUE_ID
                },
                new Issue
                {
                    IssueId = SECOND_ISSUE_ID
                }
            );
            
            context.SaveChanges();
            return context;
        }

        /// <summary>
        /// Удаление контекста базы данных.
        /// </summary>
        /// <param name="context"> Контекст БД для доставки. </param>
        public static void Destroy(DeliveryDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
