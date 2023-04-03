namespace Kiosk.Catalog.Tests.Commands.Products
{
    using Kiosk.Catalog.Tests.Common;
    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.CatalogWebApi;
    using Kiosk.Core.Dtos.Catalog;

    using Microsoft.EntityFrameworkCore;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования обновления продуктов.
    /// </summary>
    public class UpdateProductRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное обновление продукта.
        /// </summary>
        [Test]
        public async Task UpdateProduct_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new ProductRepository(Context, mapper);
            var updatedName = "Творожный сырок";
            var productId = CatalogContextFactory.FIRST_PRODUCT_ID;

            // Избавляемся от отслеживания сущностей во избежание ошибок.
            foreach (var entity in Context.ChangeTracker.Entries()) 
                entity.State = EntityState.Detached;

            var productDto = new ProductDto
            {
                BrandId = CatalogContextFactory.FIRST_BRAND_ID,
                ProductId = productId,
                ProductName = updatedName,
            };

            // Act
            await repository.UpdateProduct(productDto);

            // Assert
            Assert.NotNull(await Context.Products.SingleOrDefaultAsync(product =>
                product.ProductId == productId && product.ProductName == updatedName));
        }

        /// <summary>
        /// Ожидается неуспешное обновление продукта.
        /// </summary>
        /// <remarks> Некорректный идентификатор продукта используется. </remarks>
        [Test]
        public void UpdateProduct_FailOnWrongId()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new ProductRepository(Context, mapper);

            var product = new ProductDto()
            {
                BrandId = CatalogContextFactory.FIRST_BRAND_ID,
                ProductId = -1,
                ProductName = string.Empty,
            };

            // Act
            // Assert
            Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repository.UpdateProduct(product));
        }
    }
}
