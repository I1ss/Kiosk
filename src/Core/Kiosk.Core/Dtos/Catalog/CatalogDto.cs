namespace Kiosk.Core.Dtos.Catalog
{
    /// <summary>
    /// ДТО "каталога" для передачи через запросы.
    /// </summary>
    public class CatalogDto
    {
        /// <summary>
        /// Категории для каталога.
        /// </summary>
        public List<CategoryDto> Categories { get; set; }
    }
}
