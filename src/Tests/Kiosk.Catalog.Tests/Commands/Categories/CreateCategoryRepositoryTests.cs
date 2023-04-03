namespace Kiosk.Catalog.Tests.Commands.Categories
{
    using Kiosk.Catalog.Tests.Common;
    using Kiosk.CatalogWebApi;
    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.Core.Dtos.Catalog;

    using Microsoft.EntityFrameworkCore;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования создания категорий.
    /// </summary>
    public class CreateCategoryRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное создание категории.
        /// </summary>
        [Test]
        public async Task CreateCategory_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new CategoryRepository(Context, mapper);
            var random = new Random();
            var categoryId = random.Next(10, 20);
            var categoryName = "Молочная продукция";

            var categoryDto = new CategoryDto
            {
                CategoryId = categoryId,
                CategoryName = categoryName,
            };

            // Act
            await repository.CreateCategory(categoryDto);

            // Assert
            Assert.NotNull(await Context.Categories.SingleOrDefaultAsync(category =>
                    category.CategoryId == categoryId && category.CategoryName == categoryName));
        }
    }
}
