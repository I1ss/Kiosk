
namespace Kiosk.AuthenticationWebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Kiosk.AuthenticationWebApi.Repositories;
    using Kiosk.AuthenticationWebApi.Helpers;
    using Kiosk.JwtAuthenticationManager;
    using Kiosk.Core.Dtos.Authentication;
    using Kiosk.Core.Requests;
    using Kiosk.Core.Responses;

    /// <summary>
    /// Контроллер для пользователей.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly JwtTokenHandler _jwtTokenHandler;

        /// <summary>
        /// Репозиторий пользователей.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Конструктор контроллера для пользователей.
        /// </summary>
        /// <param name="userRepository"> Репозиторий пользователей. </param>
        public UserController(IUserRepository userRepository, JwtTokenHandler jwtTokenHandler)
        {
            _userRepository = userRepository;
            _jwtTokenHandler = jwtTokenHandler;
        }

        /// <summary>
        /// Зарегистрироваться в системе. 
        /// </summary>
        /// <param name="user"> Пользователь. </param>
        /// <response code="200"> Регистрация успешна. </response>
        /// <response code="502"> Регистрация не успешна. </response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto user)
        {
            user.IsAuthentication = await _userRepository.Register(user);
            await Authenticate(user);

            return Ok(user);
        }

        /// <summary>
        /// Аутентификация.
        /// </summary>
        /// <param name="user"> ДТО пользователя. </param>
        /// <returns> Пользователь или неудачная аутентификация. </returns>
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserDto?>> Authenticate(UserDto user)
        {
            var userWithToken = _jwtTokenHandler.GenerateJwtToken(user);
            if (userWithToken == null) 
                return Unauthorized();

            return userWithToken;
        }

        /// <summary>
        /// Войти в систему. 
        /// </summary>
        /// <param name="user"> Пользователь. </param>
        /// <response code="200"> Вход успешен. </response>
        /// <response code="502"> Войти не удалось. </response>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto user)
        {
            user.IsAuthentication = await _userRepository.Login(user);
            var userWithToken = await Authenticate(user);

            return Ok(userWithToken?.Value);
        }
    }
}
