using IslamicPOS.Core.Models.Auth;

namespace IslamicPOS.Core.Services.Auth;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(string email, string password);
    Task<AuthResult> RegisterAsync(UserRegistration registration);
    Task<AuthResult> RefreshTokenAsync(string refreshToken);
}