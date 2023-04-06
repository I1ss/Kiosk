namespace Kiosk.Core.Requests
{
    using MassTransit;

    /// <summary>
    /// Запрос для удаления задачи в доставке.
    /// </summary>
    public class DeleteIssueRequest : IConsumer
    {
        /// <summary>
        /// Идентификатор заказа, к которому привязана задача.
        /// </summary>
        public int OrderId { get; set; }
    }
}
