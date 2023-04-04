namespace Kiosk.Order.Tests.Commands.ProductsInOrder
{
    using Kiosk.Core.Dtos.Catalog;
    using Kiosk.Order.Tests.Common;
    using Kiosk.OrdersWebApi;
    using Kiosk.OrdersWebApi.Repositories;

    using Microsoft.EntityFrameworkCore;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования обновления продуктов в заказе.
    /// </summary>
    public class UpdateProductInOrderRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное обновление продуктов в заказе.
        /// </summary>
        [Test]
        public async Task UpdateOrder_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var productRepository = new ProductInOrderRepository(Context, mapper);

            // Избавляемся от отслеживания сущностей во избежание ошибок.
            foreach (var entity in Context.ChangeTracker.Entries()) 
                entity.State = EntityState.Detached;

            var productDtos = new List<ProductDto>
            {
                new ProductDto
                {
                    ProductId = OrderContextFactory.THIRD_PRODUCT_ID,
                    OrderId = OrderContextFactory.SECOND_ORDER_ID,
                    ProductCode = string.Empty,
                    ProductName = string.Empty,
                },
                new ProductDto
                {
                    ProductId = OrderContextFactory.FOURTH_PRODUCT_ID,
                    OrderId = OrderContextFactory.SECOND_ORDER_ID,
                    ProductCode = string.Empty,
                    ProductName = string.Empty,
                },
            };

            // Act
            await productRepository.UpdateProductsInOrder(productDtos);

            // Assert
            Assert.That(productDtos.Count, Is.EqualTo(Context.ProductsInOrder
                .Count(product => product.OrderId == OrderContextFactory.FIRST_ORDER_ID)));
        }
    }
}
