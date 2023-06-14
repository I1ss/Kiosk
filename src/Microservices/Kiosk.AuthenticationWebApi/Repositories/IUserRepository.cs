namespace Kiosk.AuthenticationWebApi.Repositories
{
    using Kiosk.Core.Dtos.Authentication;

    /// <summary>
    /// Репозиторий пользователей.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Зарегистрировать пользователя. 
        /// </summary>
        Task<bool> Register(UserDto user);

        /// <summary>
        /// Авторизовать пользователя. 
        /// </summary>
        Task<bool> Login(UserDto user);

        /// <summary>
        /// Получить всех пользователей из базы данных.
        /// </summary>
        /// <returns> Список пользователей. </returns>
        Task<List<UserDto>> GetUsers();
    }
}
