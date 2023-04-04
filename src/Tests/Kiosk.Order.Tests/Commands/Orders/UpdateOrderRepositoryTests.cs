namespace Kiosk.Order.Tests.Commands.Orders
{
    using Kiosk.Core.Dtos.Order;
    using Kiosk.Order.Tests.Common;
    using Kiosk.OrdersWebApi;
    using Kiosk.OrdersWebApi.Repositories;

    using Microsoft.EntityFrameworkCore;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования обновления заказов.
    /// </summary>
    public class UpdateOrderRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное обновление заказа.
        /// </summary>
        [Test]
        public async Task UpdateOrder_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var productRepository = new ProductInOrderRepository(Context, mapper);
            var orderRepository = new OrderRepository(Context, mapper, productRepository);
            var updatedTotalPrice = 555;
            var orderId = OrderContextFactory.FIRST_ORDER_ID;

            // Избавляемся от отслеживания сущностей во избежание ошибок.
            foreach (var entity in Context.ChangeTracker.Entries()) 
                entity.State = EntityState.Detached;

            var orderDto = new OrderDto
            {
                OrderId = OrderContextFactory.FIRST_ORDER_ID,
                TotalPrice = updatedTotalPrice
            };

            // Act
            await orderRepository.UpdateOrder(orderDto);

            // Assert
            Assert.NotNull(await Context.Orders.SingleOrDefaultAsync(order =>
                order.OrderId == orderId && order.TotalPrice.Equals(updatedTotalPrice)));
        }

        /// <summary>
        /// Ожидается неуспешное обновление заказа.
        /// </summary>
        /// <remarks> Некорректный идентификатор заказа используется. </remarks>
        [Test]
        public void UpdateOrder_FailOnWrongId()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var productRepository = new ProductInOrderRepository(Context, mapper);
            var orderRepository = new OrderRepository(Context, mapper, productRepository);
            var order = new OrderDto { OrderId = -1, };

            // Act
            // Assert
            Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await orderRepository.UpdateOrder(order));
        }
    }
}
