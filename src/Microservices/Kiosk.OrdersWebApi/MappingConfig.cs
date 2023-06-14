namespace Kiosk.OrdersWebApi
{
    using AutoMapper;

    using Kiosk.Core.Dtos.Catalog;
    using Kiosk.Core.Dtos.Order;
    using Kiosk.OrdersWebApi.Models;

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
                config.CreateMap<OrderDto, Order>().ReverseMap();
                config.CreateMap<ProductDto, ProductInOrder>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
