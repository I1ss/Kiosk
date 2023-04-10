namespace Kiosk.JwtAuthenticationManager
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Extensions.DependencyInjection;

    using System.Text;

    /// <summary>
    /// Переопределение для настройки сервисов аутентификации.
    /// </summary>
    public static class CustomJwtAuthExtension
    {
        /// <summary>
        /// Переопределение для настройки сервисов аутентификации.
        /// </summary>
        /// <param name="services"> Сервисы приложения. </param>
        public static void AddCustomJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtTokenHandler.JWT_SECURITY_KEY))
                };
            });
        }
    }
}
