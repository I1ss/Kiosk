namespace Kiosk.Order.Tests.Commands.Orders
{
    using Kiosk.Order.Tests.Common;
    using Kiosk.OrdersWebApi;
    using Kiosk.OrdersWebApi.Repositories;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования получения заказов.
    /// </summary>
    public class GetOrderRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное получение заказа.
        /// </summary>
        [Test]
        public async Task GetOrder_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var productRepository = new ProductInOrderRepository(Context, mapper);
            var orderRepository = new OrderRepository(Context, mapper, productRepository);

            // Act
            var order = await orderRepository.GetOrder(OrderContextFactory.FIRST_ORDER_ID);

            // Assert
            Assert.NotNull(order);
        }

        /// <summary>
        /// Ожидается успешное получение заказов.
        /// </summary>
        [Test]
        public async Task GetOrders_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var productRepository = new ProductInOrderRepository(Context, mapper);
            var orderRepository = new OrderRepository(Context, mapper, productRepository);

            // Act
            var orders = await orderRepository.GetOrders();

            // Assert
            Assert.That(orders.Count(), Is.EqualTo(Context.Orders.Count()));
        }
    }
}
