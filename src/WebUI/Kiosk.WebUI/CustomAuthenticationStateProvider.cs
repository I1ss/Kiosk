namespace Kiosk.WebUI
{
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

    using System.Security.Claims;

    using Kiosk.Core.Dtos.Authentication;

    /// <summary>
    /// Переопределенные возможности аутентификации.
    /// </summary>
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        /// <summary>
        /// Информация о сессии.
        /// </summary>
        private readonly ProtectedSessionStorage _sessionStorage;

        /// <summary>
        /// Основные поля сессии.
        /// </summary>
        private ClaimsPrincipal _anonymous;

        /// <summary>
        /// Переопределенные возможности аутентификации.
        /// </summary>
        /// <param name="sessionStorage"> Информация о сессии. </param>
        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            _sessionStorage = sessionStorage;
        }

        /// <summary>
        /// Получить состояние сессии пользователя.
        /// </summary>
        /// <returns> Состояние сессии пользователя. </returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSessionStorageResult = await _sessionStorage.GetAsync<UserDto>("UserSession");
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
                    new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userSession.Name),
                        new Claim(ClaimTypes.Role, userSession.Role.ToString())
                    }, "CustomAuth"));
            
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        /// <summary>
        /// Обновить состояние сессии пользователя.
        /// </summary>
        /// <param name="userSession"> Сессия пользователя. </param>
        public async Task UpdateAuthenticationState(UserDto userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                await _sessionStorage.SetAsync("UserSession", userSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Name),
                    new Claim(ClaimTypes.Role, userSession.Role.ToString())
                }));
            }
            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
