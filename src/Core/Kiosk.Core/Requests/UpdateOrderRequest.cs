namespace Kiosk.Core.Requests
{
    using Kiosk.Core.Dtos.Delivery;

    using MassTransit;

    /// <summary>
    /// Запрос обновления заказа.
    /// </summary>
    public class UpdateOrderRequest : IConsumer
    {
        /// <summary>
        /// Задача.
        /// </summary>
        public IssueDto Issue { get; set; }
    }
}
