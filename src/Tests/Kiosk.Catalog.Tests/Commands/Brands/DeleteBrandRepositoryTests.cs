namespace Kiosk.Catalog.Tests.Commands.Brands
{
    using Kiosk.Catalog.Tests.Common;
    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.CatalogWebApi;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования удаления брендов.
    /// </summary>
    public class DeleteBrandRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное удаление бренда.
        /// </summary>
        [Test]
        public async Task DeleteBrand_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new BrandRepository(Context, mapper);

            // Act
            await repository.DeleteBrand(CatalogContextFactory.FIRST_BRAND_ID);

            // Assert
            Assert.Null(Context.Brands.SingleOrDefault(brand =>
                brand.BrandId == CatalogContextFactory.FIRST_BRAND_ID));
        }

        /// <summary>
        /// Ожидается неуспешное удаление бренда.
        /// </summary>
        /// <remarks> Некорректный идентификатор бренда используется. </remarks>
        [Test]
        public void DeleteBrand_FailOnWrongId()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new BrandRepository(Context, mapper);

            // Act
            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await repository.DeleteBrand(-1));
        }
    }
}
