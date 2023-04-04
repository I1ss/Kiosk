namespace Kiosk.OrdersWebApi.Repositories
{
    using Kiosk.Core.Dtos.Order;

    /// <summary>
    /// Репозиторий заказов.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Получить заказ по идентификатору. 
        /// </summary>
        /// <param name="orderId"> Идентификатор заказа. </param>
        /// <remarks> Заказ берётся из базы данных. </remarks>
        /// <returns> Заказ товаров. </returns>
        Task<OrderDto> GetOrder(int orderId);

        /// <summary>
        /// Получить все заказы. 
        /// </summary>
        /// <remarks> Заказы берутся из базы данных. </remarks>
        /// <returns> Заказы товаров. </returns>
        Task<IEnumerable<OrderDto>> GetOrders();

        /// <summary>
        /// Создать новый заказ.
        /// </summary>
        /// <param name="order"> ДТО заказа. </param>
        /// <remarks> Создаётся новый заказ на основе ДТО. </remarks>
        Task CreateOrder(OrderDto order);

        /// <summary>
        /// Обновить существующий заказ.
        /// </summary>
        /// <param name="order"> ДТО заказа. </param>
        /// <remarks> Обновляется существующий заказ на основе ДТО. </remarks>
        Task UpdateOrder(OrderDto order);

        /// <summary>
        /// Удалить существующий заказ.
        /// </summary>
        /// <param name="orderId"> Идентификатор заказа. </param>
        /// <remarks> Удаляется существующий заказ на основе ДТО. </remarks>
        Task DeleteOrder(int orderId);
    }
}
