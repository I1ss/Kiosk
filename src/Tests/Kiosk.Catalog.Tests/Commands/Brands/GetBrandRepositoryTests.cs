namespace Kiosk.Catalog.Tests.Commands.Brands
{
    using Kiosk.Catalog.Tests.Common;
    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.CatalogWebApi;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования получения брендов.
    /// </summary>
    public class GetBrandRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное получение бренда.
        /// </summary>
        [Test]
        public async Task GetBrand_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new BrandRepository(Context, mapper);

            // Act
            var brand = await repository.GetBrand(CatalogContextFactory.FIRST_BRAND_ID);

            // Assert
            Assert.NotNull(brand);
        }

        /// <summary>
        /// Ожидается успешное получение брендов.
        /// </summary>
        [Test]
        public async Task GetBrands_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new BrandRepository(Context, mapper);

            // Act
            var brands = await repository.GetBrands();

            // Assert
            Assert.That(brands.Count(), Is.EqualTo(Context.Brands.Count()));
        }
    }
}
