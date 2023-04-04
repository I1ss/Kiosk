namespace Kiosk.Order.Tests.Commands.Orders
{
    using Kiosk.Order.Tests.Common;
    using Kiosk.OrdersWebApi;
    using Kiosk.Core.Dtos.Order;
    using Kiosk.OrdersWebApi.Repositories;

    using Microsoft.EntityFrameworkCore;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования создания заказов.
    /// </summary>
    public class CreateOrderRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное создание заказа.
        /// </summary>
        [Test]
        public async Task CreateOrder_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var productRepository = new ProductInOrderRepository(Context, mapper);
            var orderRepository = new OrderRepository(Context, mapper, productRepository);
            var random = new Random();
            var orderId = random.Next(10, 20);

            var orderDto = new OrderDto
            {
                OrderId = orderId
            };

            // Act
            await orderRepository.CreateOrder(orderDto);

            // Assert
            Assert.NotNull(await Context.Orders.SingleOrDefaultAsync(order => order.OrderId == orderId));
        }
    }
}
