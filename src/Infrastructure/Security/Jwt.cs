using System;

namespace src.Infrastructure.Security
{
    public abstract class Jwt
    {
        public string Token { get; protected set; }
        public long Expiration { get; protected set; }

        public Jwt(string token, long expiration)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));
            if (expiration <= 0) throw new ArgumentOutOfRangeException(nameof(expiration));

            Token = token;
            Expiration = expiration;
        }

        public bool IsExpired()
        {
            return DateTime.UtcNow.Ticks > Expiration;
        }
    }
}