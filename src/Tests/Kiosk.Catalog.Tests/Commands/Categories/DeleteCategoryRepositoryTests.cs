namespace Kiosk.Catalog.Tests.Commands.Categories
{
    using Kiosk.Catalog.Tests.Common;
    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.CatalogWebApi;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования удаления категорий.
    /// </summary>
    public class DeleteCategoryRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное удаление категории.
        /// </summary>
        [Test]
        public async Task DeleteCategory_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new CategoryRepository(Context, mapper);

            // Act
            await repository.DeleteCategory(CatalogContextFactory.FIRST_CATEGORY_ID);

            // Assert
            Assert.Null(Context.Categories.SingleOrDefault(category =>
                category.CategoryId == CatalogContextFactory.FIRST_CATEGORY_ID));
        }

        /// <summary>
        /// Ожидается неуспешное удаление категории.
        /// </summary>
        /// <remarks> Некорректный идентификатор категории используется. </remarks>
        [Test]
        public void DeleteCategory_FailOnWrongId()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new CategoryRepository(Context, mapper);

            // Act
            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await repository.DeleteCategory(-1));
        }
    }
}
