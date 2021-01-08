using System;
using System.Threading.Tasks;
using src.Domain.Models.Users;
using src.Domain.ViewModels.Users;
using src.Infrastructure.Security.Interfaces;
using src.Persistence.Repositories.UnitOfWork;
using src.Persistence.Repositories.Users.Interfaces;
using src.Persistence.Services.Interfaces.Users;

namespace src.Persistence.Services.Implements.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepo, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(passwordHasher));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task<UserResponse> CreateUserAsync(User user, params ApplicationRole[] userRoles)
        {
            var existingUser = await _userRepo.FindByUsernameAsync(user.Username);
            if (existingUser != null)
            {
                return new UserResponse(false, "Username already existed", null);
            }

            user.Password = _passwordHasher.HashPassword(user.Password);

            await _userRepo.AddAsync(user, userRoles);
            await _unitOfWork.CommitAsync();

            return new UserResponse(true, null, user);
        }

        public async Task<User> FindByUsernameAsync(string user)
        {
            return await _userRepo.FindByUsernameAsync(user);
        }
    }
}