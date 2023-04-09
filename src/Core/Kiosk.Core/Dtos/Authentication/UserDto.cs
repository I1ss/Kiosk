namespace Kiosk.Core.Dtos.Authentication
{
    using Kiosk.Core.Enums;

    /// <summary>
    /// Модель "пользователя" для базы данных.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Удалось ли аутентифицироваться?
        /// </summary>
        public bool IsAuthentication { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public UserRoleEnum Role { get; set; }
    }
}
