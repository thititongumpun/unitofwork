using System.Threading.Tasks;
using src.Domain.Models.Users;

namespace src.Persistence.Repositories.Users.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user, ApplicationRole[] userRoles);
        Task<User> FindByUsernameAsync(string username);
    }
}