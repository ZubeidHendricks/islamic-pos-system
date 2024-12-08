namespace IslamicPOS.Core.Models.Auth;

public class AuthResult
{
    public bool Success { get; set; }
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public List<string> Errors { get; set; } = new();
}
