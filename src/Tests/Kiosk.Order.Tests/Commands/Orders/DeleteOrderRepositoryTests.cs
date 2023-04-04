namespace Kiosk.Order.Tests.Commands.Orders
{
    using Kiosk.Order.Tests.Common;
    using Kiosk.OrdersWebApi;
    using Kiosk.OrdersWebApi.Repositories;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования удаления заказов.
    /// </summary>
    public class DeleteOrderRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное удаление заказа.
        /// </summary>
        [Test]
        public async Task DeleteOrder_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var productRepository = new ProductInOrderRepository(Context, mapper);
            var orderRepository = new OrderRepository(Context, mapper, productRepository);

            // Act
            await orderRepository.DeleteOrder(OrderContextFactory.FIRST_ORDER_ID);

            // Assert
            Assert.Null(Context.Orders.SingleOrDefault(order =>
                order.OrderId == OrderContextFactory.FIRST_ORDER_ID));
        }

        /// <summary>
        /// Ожидается неуспешное удаление заказа.
        /// </summary>
        /// <remarks> Некорректный идентификатор заказа используется. </remarks>
        [Test]
        public void DeleteOrder_FailOnWrongId()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var productRepository = new ProductInOrderRepository(Context, mapper);
            var orderRepository = new OrderRepository(Context, mapper, productRepository);

            // Act
            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await orderRepository.DeleteOrder(-1));
        }
    }
}
