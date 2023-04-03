namespace Kiosk.CatalogWebApi.Controllers
{
    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.Core.Dtos.Catalog;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Контроллер для каталога.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        /// <summary>
        /// Репозиторий каталога.
        /// </summary>
        private readonly ICatalogRepository _catalogRepository;

        /// <summary>
        /// Конструктор контроллера для каталога.
        /// </summary>
        /// <param name="catalogRepository"> Репозиторий каталога. </param>
        public CatalogController(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        /// <summary>
        /// Получить каталог. 
        /// </summary>
        /// <remarks> Каталог фомируется из базы данных. </remarks>
        /// <returns> Каталог товаров. </returns>
        /// <response code="200"> Каталог сформирован удачно. </response>
        /// <response code="502"> Каталог не сформирован. Проблема на стороне сервера. </response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<CatalogDto> GetCatalog()
        {
            var catalog = await _catalogRepository.GetCatalog();
            return catalog;
        }
    }
}
