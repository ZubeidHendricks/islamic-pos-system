namespace IslamicPOS.Core.Services.Auth;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(string username, string password);
    Task<AuthResult> RegisterAsync(UserRegistration registration);
    Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    Task<List<string>> GetUserPermissionsAsync(string userId);
}