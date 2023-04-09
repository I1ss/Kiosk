namespace Kiosk.OrdersWebApi.Repositories
{
    using Kiosk.Core.Dtos.Catalog;

    /// <summary>
    /// Репозиторий продуктов в заказе.
    /// </summary>
    public interface IProductInOrderRepository
    {
        /// <summary>
        /// Получить все подукты в заказе. 
        /// </summary>
        /// <remarks> Продукты в заказе берутся из базы данных. </remarks>
        /// <returns> Продукты в заказе. </returns>
        Task<IEnumerable<ProductDto>> GetProductsInOrder();

        /// <summary>
        /// Создать новые продукты в заказе.
        /// </summary>
        /// <param name="productsInOrder"> Продукты в заказе. </param>
        /// <remarks> Создаются новые продукты в заказе на основе ДТО. </remarks>
        Task CreateProductsInOrder(IEnumerable<ProductDto> productsInOrder);

        /// <summary>
        /// Удалить продукты в заказе.
        /// </summary>
        /// <param name="productsInOrder"> Продукты в заказе. </param>
        /// <remarks> Удаляются продукты в заказе на основе ДТО. </remarks>
        void DeleteProductsInOrder(IEnumerable<ProductDto> productsInOrder);

        /// <summary>
        /// Обновить существующие продукты в заказе.
        /// </summary>
        /// <param name="productsInOrder"> ДТО продуктов в заказе. </param>
        /// <remarks> Обновляются существующие продукты в заказе на основе ДТО. </remarks>
        Task UpdateProductsInOrder(List<ProductDto> productsInOrder);

        /// <summary>
        /// Очистить продукты по заданному идентификатору заказа.
        /// </summary>
        /// <param name="orderId"> Идентификатор заказа. </param>
        /// <remarks> Вариант использования: очистить заказ. </remarks>
        Task DeleteProductsInOrder(int orderId);
    }
}
