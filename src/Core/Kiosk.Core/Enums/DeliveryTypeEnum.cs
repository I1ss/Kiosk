namespace Kiosk.Core.Enums
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Описание статуса доставки.
    /// </summary>
    public enum DeliveryTypeEnum
    {
        /// <summary>
        /// Клиент получит заказ сам.
        /// </summary>
        Client,

        /// <summary>
        /// Курьер доставит заказ клиенту.
        /// </summary>
        Courier,
    }
}