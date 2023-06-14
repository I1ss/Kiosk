namespace Kiosk.Core.Dtos.Delivery
{
    using Kiosk.Core.Enums;
    using Kiosk.Core.Dtos.Catalog;

    /// <summary>
    /// ДТО "задания" для передачи через запросы.
    /// </summary>
    public class IssueDto
    {
        /// <summary>
        /// Идентификатор задания.
        /// </summary>
        public int IssueId { get; set; }

        /// <summary>
        /// Статус задания.
        /// </summary>
        public OrderStatusEnum OrderStatus { get; set; }

        /// <summary>
        /// Общая стоимость задания.
        public double TotalPrice { get; set; }

        /// <summary>
        /// Общая стоимость задания.
        /// </summary>
        public double Payment { get; set; }

        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Продукты по заданию.
        /// </summary>
        public List<ProductDto> Products { get; set; }
    }
}