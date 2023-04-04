namespace Kiosk.Core.Enums
{
    /// <summary>
    /// Описание статуса, в котором находится заказ.
    /// </summary>
    public enum OrderStatusEnum
    {
        /// <summary>
        /// Заказ принят.
        /// </summary>
        Accepted,

        /// <summary>
        /// Заказ доставляется.
        /// </summary>
        Delivers,

        /// <summary>
        /// Заказ выполнен.
        /// </summary>
        Completed,

        /// <summary>
        /// Заказ отменен.
        /// </summary>
        Canceled
    }
}
