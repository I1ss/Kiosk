namespace Kiosk.Catalog.Tests.Commands.Products
{
    using Kiosk.Catalog.Tests.Common;
    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.CatalogWebApi;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования удаления продуктов.
    /// </summary>
    public class DeleteProductRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное удаление продукта.
        /// </summary>
        [Test]
        public async Task DeleteProduct_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new ProductRepository(Context, mapper);

            // Act
            await repository.DeleteProduct(CatalogContextFactory.FIRST_PRODUCT_ID);

            // Assert
            Assert.Null(Context.Products.SingleOrDefault(product =>
                product.ProductId == CatalogContextFactory.FIRST_PRODUCT_ID));
        }

        /// <summary>
        /// Ожидается неуспешное удаление продукта.
        /// </summary>
        /// <remarks> Некорректный идентификатор продукта используется. </remarks>
        [Test]
        public void DeleteProduct_FailOnWrongId()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new ProductRepository(Context, mapper);

            // Act
            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await repository.DeleteProduct(-1));
        }
    }
}
