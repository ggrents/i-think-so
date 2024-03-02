using i_think_so.Models;
using i_think_so.Services;
using Microsoft.AspNetCore.Mvc;

namespace i_think_so.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UsersController(IUserService userService, IAuthService authService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            var existingUser = await _userService.GetUserByUsernameAsync(user.Username);
            if (existingUser != null)
            {
                return Conflict("Пользователь с таким именем уже существует");
            }

            user.SetPassword(user.PasswordHash);

            var createdUser = await _userService.CreateUserAsync(user);
            return Ok(createdUser);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(User request)
        {
            var user = await _userService.GetUserByUsernameAsync(request.Username);
            if (user == null)
            {
                return NotFound("Пользователь не найден.");
            }

            if (!user.VerifyPassword(request.PasswordHash))
            {
                return Unauthorized("Неправильное имя пользователя или пароль.");
            }

            var token = _authService.GenerateJwtTokenAsync(user);
            return Ok(new { Token = token });
        }
    }
}
