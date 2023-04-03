namespace Kiosk.Catalog.Tests.Commands.Products
{
    using Kiosk.Catalog.Tests.Common;
    using Kiosk.CatalogWebApi;
    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.Core.Dtos.Catalog;

    using Microsoft.EntityFrameworkCore;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования создания продуктов.
    /// </summary>
    public class CreateProductRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное создание продукта.
        /// </summary>
        [Test]
        public async Task CreateProduct_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new ProductRepository(Context, mapper);
            var random = new Random();
            var productId = random.Next(10, 20);
            var productName = "Йогурт";

            var productDto = new ProductDto
            {
                BrandId = CatalogContextFactory.FIRST_BRAND_ID,
                ProductId = productId,
                ProductName = productName,
                ProductCode = string.Empty,
                ProductPrice = 0
            };

            // Act
            await repository.CreateProduct(productDto);

            // Assert
            Assert.NotNull(await Context.Products.SingleOrDefaultAsync(product =>
                product.ProductId == productId && product.ProductName == productName));
        }
    }
}
