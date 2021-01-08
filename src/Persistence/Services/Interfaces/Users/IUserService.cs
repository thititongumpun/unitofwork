using System.Threading.Tasks;
using src.Domain.Models.Users;
using src.Domain.ViewModels.Users;

namespace src.Persistence.Services.Interfaces.Users
{
    public interface IUserService
    {
        Task<UserResponse> CreateUserAsync(User user, params ApplicationRole[] userRoles);
        Task<User> FindByUsernameAsync(string user);
    }
}