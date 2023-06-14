namespace Kiosk.Core.Requests
{
    using Kiosk.Core.Dtos.Order;

    using MassTransit;

    /// <summary>
    /// Создать задание для доставки.
    /// </summary>
    public class CreateIssueRequest : IConsumer
    {
        /// <summary>
        /// Заказ, для которого создаётся задание для доставки.
        /// </summary>
        public OrderDto Order { get; set; }
    }
}
