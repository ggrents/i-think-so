using i_think_so.Application.Models.DTO;
using i_think_so.Application.Models.Request;

namespace i_think_so.Application.Services.Auth
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest user, CancellationToken token);
        Task<string> LoginAsync(LoginRequest user, CancellationToken token);
        Task<UserDTO> SelfAsync(HttpContext httpContext, CancellationToken token);
    }
}
