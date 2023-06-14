namespace Kiosk.CatalogWebApi.Repositories
{
    using Kiosk.Core.Dtos.Catalog;

    /// <summary>
    /// Репозиторий категорий.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Получить категорию по идентификатору. 
        /// </summary>
        /// <param name="categoryId"> Идентификатор категории. </param>
        /// <remarks> Категория берётся из базы данных. </remarks>
        /// <returns> Категория товаров. </returns>
        Task<CategoryDto> GetCategory(int categoryId);

        /// <summary>
        /// Получить все категории. 
        /// </summary>
        /// <remarks> Категории берутся из базы данных. </remarks>
        /// <returns> Категории товаров. </returns>
        Task<IEnumerable<CategoryDto>> GetCategories();

        /// <summary>
        /// Создать новую категорию.
        /// </summary>
        /// <param name="category"> ДТО категории. </param>
        /// <remarks> Создаётся новая категория на основе ДТО. </remarks>
        Task CreateCategory(CategoryDto category);

        /// <summary>
        /// Обновить существующую категорию.
        /// </summary>
        /// <param name="category"> ДТО категории. </param>
        /// <remarks> Обновляется существующая категория на основе ДТО. </remarks>
        Task UpdateCategory(CategoryDto category);

        /// <summary>
        /// Удалить существующую категорию.
        /// </summary>
        /// <param name="categoryId"> Идентификатор категории. </param>
        /// <remarks> Удаляется существующая категория на основе ДТО. </remarks>
        Task DeleteCategory(int categoryId);
    }
}
