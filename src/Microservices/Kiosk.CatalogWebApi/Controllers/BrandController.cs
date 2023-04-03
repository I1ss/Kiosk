﻿namespace Kiosk.CatalogWebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Kiosk.CatalogWebApi.Repositories;
    using Kiosk.Core.Dtos.Catalog;

    /// <summary>
    /// Контроллер для брендов.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        /// <summary>
        /// Репозиторий брендов.
        /// </summary>
        private readonly IBrandRepository _brandRepository;

        /// <summary>
        /// Конструктор контроллера для брендов.
        /// </summary>
        /// <param name="brandRepository"> Репозиторий брендов. </param>
        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        /// <summary>
        /// Получить бренд по идентификатору. 
        /// </summary>
        /// <param name="brandId"> Идентификатор бренда. </param>
        /// <remarks> Бренд берётся из базы данных. </remarks>
        /// <returns> Бренд товаров. </returns>
        /// <response code="200"> Бренд возвращен удачно. </response>
        /// <response code="502"> Бренд не обнаружен. Проблема на стороне сервера. </response>
        [HttpGet("brand/{brandId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<BrandDto> GetCategory(int brandId)
        {
            var brand = await _brandRepository.GetBrand(brandId);
            return brand;
        }

        /// <summary>
        /// Получить все бренды. 
        /// </summary>
        /// <remarks> Бренды берутся из базы данных. </remarks>
        /// <returns> Бренды товаров. </returns>
        /// <response code="200"> Бренды возвращены удачно. </response>
        /// <response code="502"> Бренды не обнаружены. Проблема на стороне сервера. </response>
        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IEnumerable<BrandDto>> GetCategories()
        {
            var brands = await _brandRepository.GetBrands();
            return brands;
        }

        /// <summary>
        /// Создать новый бренд.
        /// </summary>
        /// <param name="brand"> ДТО бренда. </param>
        /// <remarks> Создаётся новый бренд на основе ДТО. </remarks>
        /// <response code="200"> Бренд создан удачно. </response>
        /// <response code="502"> Бренд не обнаружен. Проблема на стороне сервера. </response>
        [HttpPost("brand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> CreateCategory([FromBody]BrandDto brand)
        {
            await _brandRepository.CreateBrand(brand);
            return Ok();
        }

        /// <summary>
        /// Обновить существующий бренд.
        /// </summary>
        /// <param name="brand"> ДТО бренда. </param>
        /// <remarks> Обновляется существующий бренд на основе ДТО. </remarks>
        /// <response code="200"> Бренд обновлен удачно. </response>
        /// <response code="502"> Бренд не обновлен. Проблема на стороне сервера. </response>
        [HttpPut("brand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> UpdateCategory([FromBody]BrandDto brand)
        {
            await _brandRepository.UpdateBrand(brand);
            return Ok();
        }

        /// <summary>
        /// Удалить существующий бренд.
        /// </summary>
        /// <param name="brandId"> Идентификатор бренда. </param>
        /// <remarks> Удаляется существующий бренд на основе ДТО. </remarks>
        /// <response code="200"> Бренд удален удачно. </response>
        /// <response code="502"> Бренд не удален. Проблема на стороне сервера. </response>
        [HttpDelete("brand/{brandId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> DeleteCategory(int brandId)
        {
            await _brandRepository.DeleteBrand(brandId);
            return Ok();
        }
    }
}
