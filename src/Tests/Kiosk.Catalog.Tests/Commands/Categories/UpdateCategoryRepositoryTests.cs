namespace Kiosk.Catalog.Tests.Commands.Categories
{
    using Kiosk.Catalog.Tests.Common;
    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.CatalogWebApi;
    using Kiosk.Core.Dtos.Catalog;

    using Microsoft.EntityFrameworkCore;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования обновления категорий.
    /// </summary>
    public class UpdateCategoryRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное обновление категории.
        /// </summary>
        [Test]
        public async Task UpdateCategory_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new CategoryRepository(Context, mapper);
            var updatedName = "Мясная продукция";
            var categoryId = CatalogContextFactory.FIRST_CATEGORY_ID;

            // Избавляемся от отслеживания сущностей во избежание ошибок.
            foreach (var entity in Context.ChangeTracker.Entries()) 
                entity.State = EntityState.Detached;

            var categoryDto = new CategoryDto
            {
                CategoryId = categoryId,
                CategoryName = updatedName,
            };

            // Act
            await repository.UpdateCategory(categoryDto);

            // Assert
            Assert.NotNull(await Context.Categories.SingleOrDefaultAsync(category =>
                category.CategoryId == categoryId && category.CategoryName == updatedName));
        }

        /// <summary>
        /// Ожидается неуспешное обновление категории.
        /// </summary>
        /// <remarks> Некорректный идентификатор категории используется. </remarks>
        [Test]
        public void UpdateCategory_FailOnWrongId()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new CategoryRepository(Context, mapper);

            var category = new CategoryDto
            {
                CategoryId = -1,
                CategoryName = string.Empty,
            };

            // Act
            // Assert
            Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repository.UpdateCategory(category));
        }
    }
}
