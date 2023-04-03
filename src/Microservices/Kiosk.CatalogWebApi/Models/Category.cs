namespace Kiosk.CatalogWebApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Модель "категории" для базы данных.
    /// </summary>
    [Table("Categories")]
    public class Category
    {
        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Category_Id")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Имя категории.
        /// </summary>
        [Column("Category_Name")]
        public string CategoryName { get; set; }
    }
}
