namespace Progetto_BE_S7.DTOs.Account
{
    public class TokenResponse
    {
        public required string Token { get; set; }
        public required DateTime Expires { get; set; }
    }
}
