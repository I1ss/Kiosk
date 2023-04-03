namespace Kiosk.CatalogWebApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Модель "продукта" для базы данных.
    /// </summary>
    [Table("Products")]
    public class Product
    {
        /// <summary>
        /// Идентификатор продукта.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Product_Id")]
        public int ProductId { get; set; }

        /// <summary>
        /// Имя продукта.
        /// </summary>
        [Column("Product_Name")]
        public string ProductName { get; set; }

        /// <summary>
        /// Код продукта.
        /// </summary>
        [Column("Product_Code")]
        public string ProductCode { get; set; }

        /// <summary>
        /// Цена продукта.
        /// </summary>
        [Column("Product_Price")]
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// Идентификатор бренда, к которому принадлежит продукт.
        /// </summary>
        [Column("Brand_Id")]
        public int BrandId { get; set; }

        /// <summary>
        /// Бренд, к которому принадлежит продукт.
        /// </summary>
        [Column("Brand")]
        public Brand Brand { get; set; }
    }
}
