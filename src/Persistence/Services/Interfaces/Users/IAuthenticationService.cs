using System.Threading.Tasks;
using src.Domain.ViewModels.Users;

namespace src.Persistence.Services.Interfaces.Users
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> CreateAccessTokenAsync(string username, string password);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken, string userName);
        void RevokeRefreshToken(string refreshToken);
    }
}