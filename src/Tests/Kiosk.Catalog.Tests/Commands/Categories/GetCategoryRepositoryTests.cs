namespace Kiosk.Catalog.Tests.Commands.Categories
{
    using Kiosk.Catalog.Tests.Common;
    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.CatalogWebApi;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования получения категорий.
    /// </summary>
    public class GetCategoryRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное получение категории.
        /// </summary>
        [Test]
        public async Task GetCategory_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new CategoryRepository(Context, mapper);

            // Act
            var category = await repository.GetCategory(CatalogContextFactory.FIRST_CATEGORY_ID);

            // Assert
            Assert.NotNull(category);
        }

        /// <summary>
        /// Ожидается успешное получение категорий.
        /// </summary>
        [Test]
        public async Task GetCategories_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new CategoryRepository(Context, mapper);

            // Act
            var categories = await repository.GetCategories();

            // Assert
            Assert.That(categories.Count(), Is.EqualTo(Context.Categories.Count()));
        }
    }
}
