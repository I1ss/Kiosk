namespace Kiosk.Core.Requests
{
    using Kiosk.Core.Dtos.Order;

    using MassTransit;

    /// <summary>
    /// Обновление задачи на доставку.
    /// </summary>
    public class UpdateIssueRequest : IConsumer
    {
        /// <summary>
        /// Заказ, к которому привязана задача.
        /// </summary>
        public OrderDto Order { get; set; }
    }
}
