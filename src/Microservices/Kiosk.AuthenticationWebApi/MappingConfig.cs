namespace Kiosk.AuthenticationWebApi
{
    using AutoMapper;

    using Kiosk.AuthenticationWebApi.Models;
    using Kiosk.Core.Dtos.Authentication;

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
                config.CreateMap<UserDto, User>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
