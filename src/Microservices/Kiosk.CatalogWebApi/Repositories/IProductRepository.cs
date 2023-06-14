namespace Kiosk.CatalogWebApi.Repositories
{
    using Kiosk.Core.Dtos.Catalog;

    /// <summary>
    /// Репозиторий товаров.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Получить продукт по идентификатору. 
        /// </summary>
        /// <param name="productId"> Идентификатор продукта. </param>
        /// <remarks> Продукт берётся из базы данных. </remarks>
        /// <returns> Продукт товаров. </returns>
        Task<ProductDto> GetProduct(int productId);

        /// <summary>
        /// Получить все продукты. 
        /// </summary>
        /// <remarks> Продукты берутся из базы данных. </remarks>
        /// <returns> Продукты товаров. </returns>
        Task<IEnumerable<ProductDto>> GetProducts();

        /// <summary>
        /// Создать новый продукт.
        /// </summary>
        /// <param name="product"> ДТО продукта. </param>
        /// <remarks> Создаётся новый продукт на основе ДТО. </remarks>
        Task CreateProduct(ProductDto product);

        /// <summary>
        /// Обновить существующий продукт.
        /// </summary>
        /// <param name="product"> ДТО продукта. </param>
        /// <remarks> Обновляется существующий продукт на основе ДТО. </remarks>
        Task UpdateProduct(ProductDto product);

        /// <summary>
        /// Удалить существующий продукт.
        /// </summary>
        /// <param name="productId"> Идентификатор продукта. </param>
        /// <remarks> Удаляется существующий бренд на основе ДТО. </remarks>
        Task DeleteProduct(int productId);
    }
}
