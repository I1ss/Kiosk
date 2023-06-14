namespace Kiosk.Catalog.Tests.Commands.Brands
{
    using Kiosk.Catalog.Tests.Common;
    using Kiosk.CatalogWebApi;
    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.Core.Dtos.Catalog;

    using Microsoft.EntityFrameworkCore;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования создания брендов.
    /// </summary>
    public class CreateBrandRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное создание бренда.
        /// </summary>
        [Test]
        public async Task CreateBrand_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new BrandRepository(Context, mapper);
            var random = new Random();
            var brandId = random.Next(10, 20);
            var brandName = "Danone";

            var brandDto = new BrandDto
            {
                CategoryId = CatalogContextFactory.FIRST_CATEGORY_ID,
                BrandId = brandId,
                BrandName = brandName,
            };

            // Act
            await repository.CreateBrand(brandDto);

            // Assert
            Assert.NotNull(await Context.Brands.SingleOrDefaultAsync(brand =>
                brand.BrandId == brandId && brand.BrandName == brandName));
        }
    }
}
