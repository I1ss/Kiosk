namespace Kiosk.Catalog.Tests.Commands.Brands
{
    using Kiosk.Catalog.Tests.Common;
    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.CatalogWebApi;
    using Kiosk.Core.Dtos.Catalog;

    using Microsoft.EntityFrameworkCore;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования обновления брендов.
    /// </summary>
    public class UpdateBrandRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное обновление бренда.
        /// </summary>
        [Test]
        public async Task UpdateBrand_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new BrandRepository(Context, mapper);
            var updatedName = "Торговая площадь";
            var brandId = CatalogContextFactory.FIRST_BRAND_ID;

            // Избавляемся от отслеживания сущностей во избежание ошибок.
            foreach (var entity in Context.ChangeTracker.Entries()) 
                entity.State = EntityState.Detached;

            var brandDto = new BrandDto()
            {
                CategoryId = CatalogContextFactory.FIRST_CATEGORY_ID,
                BrandId = brandId,
                BrandName = updatedName,
            };

            // Act
            await repository.UpdateBrand(brandDto);

            // Assert
            Assert.NotNull(await Context.Brands.SingleOrDefaultAsync(brand =>
                brand.BrandId == brandId && brand.BrandName == updatedName));
        }

        /// <summary>
        /// Ожидается неуспешное обновление бренда.
        /// </summary>
        /// <remarks> Некорректный идентификатор бренда используется. </remarks>
        [Test]
        public void UpdateBrand_FailOnWrongId()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new BrandRepository(Context, mapper);

            var brand = new BrandDto
            {
                CategoryId = CatalogContextFactory.FIRST_CATEGORY_ID,
                BrandId = -1,
                BrandName = string.Empty,
            };

            // Act
            // Assert
            Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repository.UpdateBrand(brand));
        }
    }
}
