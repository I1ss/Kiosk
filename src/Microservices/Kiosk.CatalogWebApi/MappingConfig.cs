namespace Kiosk.CatalogWebApi
{
    using AutoMapper;

    using Kiosk.CatalogWebApi.Models;
    using Kiosk.Core.Dtos.Catalog;

    /// <summary>
    /// Класс для генерации конфигурации маппера.
    /// </summary>
    public class MappingConfig
    {
        /// <summary>
        /// Зарегистрировать конфигурацию для маппера.
        /// </summary>
        /// <returns> Конфигурация маппера. </returns>
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CategoryDto, Category>().ReverseMap();
                config.CreateMap<BrandDto, Brand>().ReverseMap();
                config.CreateMap<ProductDto, Product>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
