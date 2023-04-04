namespace Kiosk.OrdersWebApi.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using Kiosk.Core.Enums;

    /// <summary>
    /// Модель "заказа" для базы данных.
    /// </summary>
    [Table("Orders")]
    public class Order
    {
        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Order_Id")]
        public int OrderId { get; set; }

        /// <summary>
        /// Тип доставки.
        /// </summary>
        [Column("Delivery_Type")]
        public DeliveryTypeEnum DeliveryType { get; set; }

        /// <summary>
        /// Статус заказа.
        /// </summary>
        [Column("Order_Status")]
        public OrderStatusEnum OrderStatus { get; set; }

        /// <summary>
        /// Общая стоимость заказа.
        /// </summary>
        [Column("Order_TotalPrice")]
        public double TotalPrice { get; set; }
    }
}
