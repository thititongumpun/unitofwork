namespace src.Infrastructure.Security
{
    public class RefreshToken : Jwt
    {
        public RefreshToken(string token, long expiration) : base(token, expiration) {}
    }
}