namespace Kiosk.CatalogWebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.Core.Dtos.Catalog;

    /// <summary>
    /// Контроллер для продуктов.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Репозиторий продуктов.
        /// </summary>
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Конструктор контроллера для продуктов.
        /// </summary>
        /// <param name="productRepository"> Репозиторий продуктов. </param>
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Получить продукт по идентификатору. 
        /// </summary>
        /// <param name="productId"> Идентификатор продукта. </param>
        /// <remarks> Продукт берётся из базы данных. </remarks>
        /// <returns> Продукт товаров. </returns>
        /// <response code="200"> Продукт возвращен удачно. </response>
        /// <response code="502"> Продукт не обнаружен. Проблема на стороне сервера. </response>
        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [Authorize]
        public async Task<ProductDto> GetProduct(int productId)
        {
            var product = await _productRepository.GetProduct(productId);
            return product;
        }

        /// <summary>
        /// Получить все продукты. 
        /// </summary>
        /// <remarks> Продукты берутся из базы данных. </remarks>
        /// <returns> Продукты товаров. </returns>
        /// <response code="200"> Продукты возвращены удачно. </response>
        /// <response code="502"> Продукты не обнаружены. Проблема на стороне сервера. </response>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [Authorize]
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return products;
        }

        /// <summary>
        /// Создать новый продукт.
        /// </summary>
        /// <param name="product"> ДТО продукта. </param>
        /// <remarks> Создаётся новый продукт на основе ДТО. </remarks>
        /// <response code="200"> Продукт создан удачно. </response>
        /// <response code="502"> Продукт не обнаружен. Проблема на стороне сервера. </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> CreateProduct([FromBody]ProductDto product)
        {
            await _productRepository.CreateProduct(product);
            return Ok();
        }

        /// <summary>
        /// Обновить существующий продукт.
        /// </summary>
        /// <param name="product"> ДТО продукта. </param>
        /// <remarks> Обновляется существующий продукт на основе ДТО. </remarks>
        /// <response code="200"> Продукт обновлен удачно. </response>
        /// <response code="502"> Продукт не обновлен. Проблема на стороне сервера. </response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> UpdateProduct([FromBody]ProductDto product)
        {
            await _productRepository.UpdateProduct(product);
            return Ok();
        }

        /// <summary>
        /// Удалить существующий продукт.
        /// </summary>
        /// <param name="productId"> Идентификатор продукта. </param>
        /// <remarks> Удаляется существующий продукт на основе ДТО. </remarks>
        /// <response code="200"> Продукт удален удачно. </response>
        /// <response code="502"> Продукт не удален. Проблема на стороне сервера. </response>
        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _productRepository.DeleteProduct(productId);
            return Ok();
        }
    }
}
