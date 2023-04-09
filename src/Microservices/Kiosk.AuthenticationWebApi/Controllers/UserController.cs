﻿namespace Kiosk.AuthenticationWebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Kiosk.AuthenticationWebApi.Repositories;
    using Kiosk.Core.Dtos.Authentication;

    /// <summary>
    /// Контроллер для пользователей.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        /// <summary>
        /// Репозиторий пользователей.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Конструктор контроллера для пользователей.
        /// </summary>
        /// <param name="userRepository"> Репозиторий пользователей. </param>
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            return Ok(user);
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
            return Ok(user);
        }
    }
}
