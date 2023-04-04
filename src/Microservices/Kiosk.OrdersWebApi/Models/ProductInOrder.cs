namespace Kiosk.OrdersWebApi.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Модель "продукта в заказе" для базы данных.
    /// </summary>
    [Table("ProductsInOrder")]
    public class ProductInOrder
    {
        /// <summary>
        /// Идентификатор продукта в заказе.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ProductInOrder_Id")]
        public int ProductId { get; set; }

        /// <summary>
        /// Имя продукта в заказе.
        /// </summary>
        [Column("ProductInOrder_Name")]
        public string ProductName { get; set; }

        /// <summary>
        /// Код продукта в заказе.
        /// </summary>
        [Column("ProductInOrder_Code")]
        public string ProductCode { get; set; }

        /// <summary>
        /// Цена продукта в заказе.
        /// </summary>
        [Column("ProductInOrder_Price")]
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// Идентификатор заказа, к которому принадлежит продукт.
        /// </summary>
        [Column("Order_Id")]
        public int OrderId { get; set; }

        /// <summary>
        /// Заказ, к которому принадлежит продукт.
        /// </summary>
        [Column("Order")]
        public Order Order { get; set; }
    }
}
