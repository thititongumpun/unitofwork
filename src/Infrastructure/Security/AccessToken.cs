using System;

namespace src.Infrastructure.Security
{
    public class AccessToken : Jwt
    {
        public RefreshToken RefreshToken { get; private set; }

        public AccessToken(string token, long expiration, RefreshToken refreshToken) : base(token, expiration)
        {
            if(refreshToken == null)
                throw new ArgumentException("Specify a valid refresh token.");
                
            RefreshToken = refreshToken;
        }
    }
}