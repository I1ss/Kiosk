namespace Kiosk.Order.Tests.Commands.ProductsInOrder
{
    using Kiosk.Core.Dtos.Catalog;
    using Kiosk.Order.Tests.Common;
    using Kiosk.OrdersWebApi;
    using Kiosk.OrdersWebApi.Repositories;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования создания продукта в заказе.
    /// </summary>
    public class CreateProductInOrderRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное создание продукта в заказе.
        /// </summary>
        [Test]
        public async Task CreateProductsInOrder_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new ProductInOrderRepository(Context, mapper);
            var random = new Random();

            var productDtos = new List<ProductDto>
            {
                new ProductDto
                {
                    ProductId = random.Next(10, 20),
                    ProductCode = string.Empty,
                    ProductName = string.Empty,
                },
                new ProductDto
                {
                    ProductId = random.Next(30, 40),
                    ProductCode = string.Empty,
                    ProductName = string.Empty,
                },
            };

            // Act
            await repository.CreateProductsInOrder(productDtos);

            // Assert
            Assert.That(Context.ProductsInOrder.Count().Equals(6));
        }
    }
}
