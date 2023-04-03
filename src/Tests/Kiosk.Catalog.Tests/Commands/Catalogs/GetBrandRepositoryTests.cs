namespace Kiosk.Catalog.Tests.Commands.Catalogs
{
    using Kiosk.Catalog.Tests.Common;
    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.CatalogWebApi;

    using NUnit.Framework;

    /// <summary>
    /// Класс для тестирования получения каталогов.
    /// </summary>
    public class GetCatalogRepositoryTests : TestCommandBase
    {
        /// <summary>
        /// Ожидается успешное получение каталога.
        /// </summary>
        [Test]
        public async Task GetCatalog_Success()
        {
            // Arrange
            var mapper = MappingConfig.RegisterMaps().CreateMapper();
            var repository = new CatalogRepository(Context, mapper);

            // Act
            var catalog = await repository.GetCatalog();

            // Assert
            Assert.NotNull(catalog);
        }
    }
}
