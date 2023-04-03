namespace Kiosk.CatalogWebApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Модель "бренда" для базы данных.
    /// </summary>
    [Table("Brands")]
    public class Brand
    {
        /// <summary>
        /// Идентификатор бренда.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Brand_Id")]
        public int BrandId { get; set; }

        /// <summary>
        /// Имя бренда.
        /// </summary>
        [Column("Brand_Name")]
        public string BrandName { get; set; }

        /// <summary>
        /// Идентификатор категории, к которой принадлежит бренд.
        /// </summary>
        [Column("Category_Id")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Категория, к которой принадлежит бренд.
        /// </summary>
        [Column("Category")]
        public Category? Category { get; set; }
    }
}
