namespace Kiosk.DeliveryWebApi
{
    using AutoMapper;

    using Kiosk.Core.Dtos.Delivery;
    using Kiosk.DeliveryWebApi.Models;

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
                config.CreateMap<IssueDto, Issue>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
