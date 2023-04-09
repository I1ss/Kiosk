namespace Kiosk.Core.Dtos.Order
{
    using Kiosk.Core.Enums;
    using Kiosk.Core.Dtos.Catalog;

    /// <summary>
    /// ДТО "заказа" для передачи через запросы.
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Тип доставки.
        /// </summary>
        public DeliveryTypeEnum DeliveryType { get; set; }

        /// <summary>
        /// Статус заказа.
        /// </summary>
        public OrderStatusEnum OrderStatus { get; set; }

        /// <summary>
        /// Общая стоимость заказа.
        /// </summary>
        public double TotalPrice { get; set; }

        /// <summary>
        /// Продукты в заказе.
        /// </summary>
        public List<ProductDto> Products { get; set; }
    }
}
