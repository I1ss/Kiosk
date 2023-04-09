namespace Kiosk.AuthenticationWebApi.Helpers
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Вспомогательный класс для хэширования пароля.
    /// </summary>
    public static class HashPasswordHelper
    {
        /// <summary>
        /// Хэширование пароля.
        /// </summary>
        /// <param name="password"> Пароль. </param>
        /// <returns> Захэшированный пароль. </returns>
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hash;
            }
        }
    }
}
