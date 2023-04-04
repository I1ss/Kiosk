namespace Kiosk.Core.Dtos.Catalog
{
    /// <summary>
    /// ДТО "продукта" для передачи через запросы.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Идентификатор продукта.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Имя продукта.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Код продукта.
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// Цена продукта.
        /// </summary>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// Идентификатор бренда, к которому принадлежит продукт.
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// Идентификатор заказа, к которому принадлежит продукт.
        /// </summary>
        public int OrderId { get; set; }
    }
}
