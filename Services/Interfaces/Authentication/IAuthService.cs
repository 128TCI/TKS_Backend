using Services.DTOs.User;
using static Services.DTOs.User.LoginDTO;

namespace Services.Interfaces.Authentication
{
    public interface IAuthService
    {
        Task<LoginResponseDTO?> LoginAsync(LoginDTO loginDto, CancellationToken ct);
        Task<bool> IsSuspended(string UserName);
    }
}
