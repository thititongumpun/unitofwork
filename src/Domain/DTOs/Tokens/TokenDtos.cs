namespace src.Domain.DTOs.Token
{
    public class TokenDtos
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long Expiration { get; set; }
    }
}