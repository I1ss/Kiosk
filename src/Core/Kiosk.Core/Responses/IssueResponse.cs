namespace Kiosk.Core.Responses
{
    using Kiosk.Core.Dtos.Delivery;

    /// <summary>
    /// Возвращаемое значение по запросам взаимодействия с задачами по заказу.
    /// </summary>
    public class IssueResponse
    {
        /// <summary>
        /// Задача.
        /// </summary>
        public IssueDto Issue { get; set; }
    }
}
