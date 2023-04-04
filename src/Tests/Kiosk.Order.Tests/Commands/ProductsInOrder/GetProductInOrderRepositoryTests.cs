namespace Kiosk.Order.Tests.Commands.ProductsInOrder
{
    using Kiosk.Order.Tests.Common;
    using Kiosk.OrdersWebApi;
    using Kiosk.OrdersWebApi.Repositories;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования получения продуктов в заказе.
    /// </summary>
    public class GetProductInOrderRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное получение продуктов в заказе.
        /// </summary>
        [Test]
        public async Task GetOrders_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var productRepository = new ProductInOrderRepository(Context, mapper);

            // Act
            var productDtos = await productRepository.GetProductsInOrder();

            // Assert
            Assert.That(productDtos.Count(), Is.EqualTo(Context.ProductsInOrder.Count()));
        }
    }
}
