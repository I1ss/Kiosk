namespace Kiosk.CatalogWebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.Core.Dtos.Catalog;

    /// <summary>
    /// Контроллер для категорий.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Seller")]
    public class CategoryController : ControllerBase
    {
        /// <summary>
        /// Репозиторий категорий.
        /// </summary>
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Конструктор контроллера для категорий.
        /// </summary>
        /// <param name="categoryRepository"> Репозиторий категорий. </param>
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Получить категорию по идентификатору. 
        /// </summary>
        /// <param name="categoryId"> Идентификатор категории. </param>
        /// <remarks> Категория берётся из базы данных. </remarks>
        /// <returns> Категория товаров. </returns>
        /// <response code="200"> Категория возвращена удачно. </response>
        /// <response code="502"> Категория не обнаружена. Проблема на стороне сервера. </response>
        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<CategoryDto> GetCategory(int categoryId)
        {
            var category = await _categoryRepository.GetCategory(categoryId);
            return category;
        }

        /// <summary>
        /// Получить все категории. 
        /// </summary>
        /// <remarks> Категории берутся из базы данных. </remarks>
        /// <returns> Категории товаров. </returns>
        /// <response code="200"> Категории возвращены удачно. </response>
        /// <response code="502"> Категории не обнаружены. Проблема на стороне сервера. </response>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            return categories;
        }

        /// <summary>
        /// Создать новую категорию.
        /// </summary>
        /// <param name="category"> ДТО категории. </param>
        /// <remarks> Создаётся новая категория на основе ДТО. </remarks>
        /// <response code="200"> Категория создана удачно. </response>
        /// <response code="502"> Категория не обнаружена. Проблема на стороне сервера. </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> CreateCategory([FromBody]CategoryDto category)
        {
            await _categoryRepository.CreateCategory(category);
            return Ok();
        }

        /// <summary>
        /// Обновить существующую категорию.
        /// </summary>
        /// <param name="category"> ДТО категории. </param>
        /// <remarks> Обновляется существующая категория на основе ДТО. </remarks>
        /// <response code="200"> Категория обновлена удачно. </response>
        /// <response code="502"> Категория не обновлена. Проблема на стороне сервера. </response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> UpdateCategory([FromBody]CategoryDto category)
        {
            await _categoryRepository.UpdateCategory(category);
            return Ok();
        }

        /// <summary>
        /// Удалить существующую категорию.
        /// </summary>
        /// <param name="categoryId"> Идентификатор категории. </param>
        /// <remarks> Удаляется существующая категория на основе ДТО. </remarks>
        /// <response code="200"> Категория удалена удачно. </response>
        /// <response code="502"> Категория не удалена. Проблема на стороне сервера. </response>
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            await _categoryRepository.DeleteCategory(categoryId);
            return Ok();
        }
    }
}
