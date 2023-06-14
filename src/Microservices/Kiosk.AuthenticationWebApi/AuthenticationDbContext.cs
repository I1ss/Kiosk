namespace Kiosk.AuthenticationWebApi
{
    using Kiosk.AuthenticationWebApi.Models;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Контекст БД для аутентификации.
    /// </summary>
    public class AuthenticationDbContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста БД для аутентификации.
        /// </summary>
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> dbsOptions) : base(dbsOptions) { }

        /// <summary>
        /// Пользователи.
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
