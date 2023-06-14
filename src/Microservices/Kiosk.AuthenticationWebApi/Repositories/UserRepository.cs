namespace Kiosk.AuthenticationWebApi.Repositories
{
    using AutoMapper;

    using Kiosk.AuthenticationWebApi.Helpers;
    using Kiosk.AuthenticationWebApi.Models;
    using Kiosk.Core.Dtos.Authentication;

    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc cref="IUserRepository"/>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly AuthenticationDbContext _dbaAuthenticationDbContext;

        /// <summary>
        /// Маппер.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор репозитория пользователей.
        /// </summary>
        /// <param name="dbaAuthenticationDbContext"> Контекст базы данных. </param>
        /// <param name="mapper"> Маппер. </param>
        public UserRepository(AuthenticationDbContext dbaAuthenticationDbContext, IMapper mapper)
        {
            _dbaAuthenticationDbContext = dbaAuthenticationDbContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<bool> Register(UserDto userDto)
        {
            userDto.Password = HashPasswordHelper.HashPassword(userDto.Password);
            var user = await _dbaAuthenticationDbContext.Users.FirstOrDefaultAsync(x => x.Name == userDto.Name);
            if (user != null)
                return false;

            user = _mapper.Map<User>(userDto);
            await _dbaAuthenticationDbContext.Users.AddAsync(user);
            await _dbaAuthenticationDbContext.SaveChangesAsync();
            userDto.Role = user.Role;

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> Login(UserDto userDto)
        {
            var user = await _dbaAuthenticationDbContext.Users.FirstOrDefaultAsync(x => x.Name == userDto.Name);
            if (user == null)
                return false;
            

            if (user.Password != HashPasswordHelper.HashPassword(userDto.Password))
                return false;

            userDto.Role = user.Role;
            return true;
        }

        /// <inheritdoc />
        public async Task<List<UserDto>> GetUsers()
        {
            var usersDb = await _dbaAuthenticationDbContext.Users.ToListAsync();
            var users = _mapper.Map<List<UserDto>>(usersDb);

            return users;
        }
    }
}
