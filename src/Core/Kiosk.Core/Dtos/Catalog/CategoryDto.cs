namespace Kiosk.Core.Dtos.Catalog
{
    /// <summary>
    /// ДТО "категории" для передачи через запросы.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Имя категории.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Бренды для этой категории.
        /// </summary>
        public List<BrandDto> Brands { get; set; }
    }
}
