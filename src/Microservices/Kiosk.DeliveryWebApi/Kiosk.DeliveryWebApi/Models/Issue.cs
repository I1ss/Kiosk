namespace Kiosk.DeliveryWebApi.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using Kiosk.Core.Enums;

    /// <summary>
    /// Модель "задания" для базы данных.
    /// </summary>
    [Table("Issues")]
    public class Issue
    {
        /// <summary>
        /// Идентификатор задания.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Issue_Id")]
        public int IssueId { get; set; }

        /// <summary>
        /// Статус задания.
        /// </summary>
        [Column("Issue_Status")]
        public OrderStatusEnum OrderStatus { get; set; }

        /// <summary>
        /// Общая стоимость задания.
        /// </summary>
        [Column("Issue_TotalPrice")]
        public double TotalPrice { get; set; }

        /// <summary>
        /// Общая стоимость задания.
        /// </summary>
        [Column("Issue_Payment")]
        public double Payment { get; set; }

        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Order_Id")]
        public int OrderId { get; set; }
    }
}
