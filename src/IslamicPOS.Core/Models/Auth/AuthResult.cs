namespace IslamicPOS.Core.Models.Auth
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public string Error { get; set; }
    }
}