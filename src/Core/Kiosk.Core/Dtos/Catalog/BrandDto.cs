namespace Kiosk.Core.Dtos.Catalog
{
    /// <summary>
    /// ДТО "бренда" для передачи через запросы.
    /// </summary>
    public class BrandDto
    {
        /// <summary>
        /// Идентификатор бренда.
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// Имя бренда.
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Идентификатор категории, к которой принадлежит бренд.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Продукты для этого бренда.
        /// </summary>
        public List<ProductDto> Products { get; set; }
    }
}
