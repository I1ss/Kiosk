namespace Kiosk.Order.Tests.Commands.ProductsInOrder
{
    using Kiosk.Order.Tests.Common;
    using Kiosk.OrdersWebApi;
    using Kiosk.OrdersWebApi.Repositories;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования удаления продукта из заказа.
    /// </summary>
    public class DeleteProductInOrderRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное удаление продукта из заказа.
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
            await orderRepository.DeleteOrder(OrderContextFactory.SECOND_ORDER_ID);

            // Assert
            Assert.That(Context.ProductsInOrder.Count().Equals(0));
        }
    }
}
