namespace Kiosk.Core.Responses
{
    using Kiosk.Core.Dtos.Catalog;

    /// <summary>
    /// Возвращаемое значение по запросу получения продуктов в заказе.
    /// </summary>
    public class ProductsInOrderResponse
    {
        /// <summary>
        /// Продукты в заказе.
        /// </summary>
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
