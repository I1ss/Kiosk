namespace Kiosk.CatalogWebApi.Repositories
{
    using Kiosk.Core.Dtos.Catalog;

    /// <summary>
    /// Репозиторий брендов.
    /// </summary>
    public interface IBrandRepository
    {
        /// <summary>
        /// Получить бренд по идентификатору. 
        /// </summary>
        /// <param name="brandId"> Идентификатор бренда. </param>
        /// <remarks> Бренд берётся из базы данных. </remarks>
        /// <returns> Бренд товаров. </returns>
        Task<BrandDto> GetBrand(int brandId);

        /// <summary>
        /// Получить все бренды. 
        /// </summary>
        /// <remarks> Бренды берутся из базы данных. </remarks>
        /// <returns> Бренды товаров. </returns>
        Task<IEnumerable<BrandDto>> GetBrands();

        /// <summary>
        /// Создать новый бренд.
        /// </summary>
        /// <param name="brand"> ДТО бренда. </param>
        /// <remarks> Создаётся новый бренд на основе ДТО. </remarks>
        Task CreateBrand(BrandDto brand);

        /// <summary>
        /// Обновить существующий бренд.
        /// </summary>
        /// <param name="brand"> ДТО бренда. </param>
        /// <remarks> Обновляется существующий бренд на основе ДТО. </remarks>
        Task UpdateBrand(BrandDto brand);

        /// <summary>
        /// Удалить существующий бренд.
        /// </summary>
        /// <param name="brandId"> Идентификатор бренда. </param>
        /// <remarks> Удаляется существующий бренд на основе ДТО. </remarks>
        /// <response code="200"> Бренд удален удачно. </response>
        /// <response code="502"> Бренд не удален. Проблема на стороне сервера. </response>
        Task DeleteBrand(int brandId);
    }
}
