namespace Kiosk.Core.Responses
{
    using Kiosk.Core.Dtos.Order;

    /// <summary>
    /// Возвращаемое значение по запросу взаимодействия с заказом.
    /// </summary>
    public class OrderResponse
    {
        /// <summary>
        /// Заказ.
        /// </summary>
        public OrderDto Order { get; set; }
    }
}
