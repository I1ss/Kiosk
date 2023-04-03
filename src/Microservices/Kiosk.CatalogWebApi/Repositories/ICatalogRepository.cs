namespace Kiosk.CatalogWebApi.Repositories
{
    using Kiosk.Core.Dtos.Catalog;

    /// <summary>
    /// Репозиторий каталога.
    /// </summary>
    public interface ICatalogRepository
    {
        /// <summary>
        /// Получить каталог. 
        /// </summary>
        /// <remarks> Каталог формируется из базы данных. </remarks>
        /// <returns> Каталог товаров. </returns>
        Task<CatalogDto> GetCatalog();
    }
}
