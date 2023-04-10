namespace Kiosk.JwtAuthenticationManager
{
    using Kiosk.Core.Dtos.Authentication;

    using Microsoft.IdentityModel.Tokens;

    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    /// <summary>
    /// Класс для генерации токена.
    /// </summary>
    public class JwtTokenHandler
    {
        /// <summary>
        /// Шифрование.
        /// </summary>
        public const string JWT_SECURITY_KEY = "yPkCqn4kSWLtaJwXvN2jGzpQRyTZ3gdXkt7FeBJP";

        /// <summary>
        /// Время жизни токена.
        /// </summary>
        private const int JWT_TOKEN_VALIDITY_MINS = 20;

        /// <summary>
        /// Генерация токена.
        /// </summary>
        /// <param name="userAccount"> ДТО пользователя. </param>
        /// <returns> ДТО пользователя с токеном. </returns>
        public UserDto? GenerateJwtToken(UserDto userAccount)
        {
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, userAccount.Name),
                new Claim("role", userAccount.Role.ToString())
            });

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            userAccount.Token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return userAccount;
        }
    }
}
