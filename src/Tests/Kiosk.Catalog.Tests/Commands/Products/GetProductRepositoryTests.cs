namespace Kiosk.Catalog.Tests.Commands.Products
{
    using Kiosk.Catalog.Tests.Common;
    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.CatalogWebApi;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования получения продуктов.
    /// </summary>
    public class GetProductRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное получение продукта.
        /// </summary>
        [Test]
        public async Task GetProduct_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new ProductRepository(Context, mapper);

            // Act
            var product = await repository.GetProduct(CatalogContextFactory.FIRST_PRODUCT_ID);

            // Assert
            Assert.NotNull(product);
        }

        /// <summary>
        /// Ожидается успешное получение продуктов.
        /// </summary>
        [Test]
        public async Task GetProducts_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new ProductRepository(Context, mapper);

            // Act
            var products = await repository.GetProducts();

            // Assert
            Assert.That(products.Count(), Is.EqualTo(Context.Products.Count()));
        }
    }
}
