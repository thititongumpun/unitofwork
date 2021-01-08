using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Moq;
using src.Domain.Models.Users;
using src.Infrastructure.Security.Interfaces;
using src.Persistence.Repositories.UnitOfWork;
using src.Persistence.Repositories.Users.Interfaces;
using src.Persistence.Services.Implements.Users;
using src.Persistence.Services.Interfaces.Users;
using Xunit;

namespace UnitTests.Services.AuthenticationTests
{
    public class UserServiceTests
    {
        private Mock<IPasswordHasher> _passwordHasher;
        private Mock<IUserRepository> _userRepository;
        private Mock<IUnitOfWork> _unitOfWork;

        private IUserService _userService;

        public UserServiceTests()
        {
            SetupMocks();
            _userService = new UserService(_userRepository.Object, _unitOfWork.Object, _passwordHasher.Object);
        }

        private void SetupMocks()
        {
            _passwordHasher = new Mock<IPasswordHasher>();
            _passwordHasher.Setup(ph => ph.HashPassword(It.IsAny<string>())).Returns("123");

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(r => r.FindByUsernameAsync("ThisIsUserForTest"))
                .ReturnsAsync(new User { Id = Guid.NewGuid(), Username = "ThisIsUserForTest", UserRoles = new Collection<UserRole>() });

            _userRepository.Setup(r => r.FindByUsernameAsync("ThisIsSecondUserForTest"))
                .Returns(Task.FromResult<User>(null));

            _userRepository.Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<ApplicationRole[]>())).Returns(Task.CompletedTask);

            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);
        }

        [Fact]
        public async Task Should_Create_Non_Existing_User()
        {
            var user = new User { Username = "testUser", Password = "123", UserRoles = new Collection<UserRole>() };
            
            var response = await _userService.CreateUserAsync(user, ApplicationRole.User);

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal(user.Username, response.User.Username);
            Assert.Equal(user.Password, response.User.Password);
        }

        [Fact]
        public async Task Should_Not_Create_User_When_User_Is_Alreary_In_Use()
        {
            var user = new User { Username = "ThisIsUserForTest", Password = "123", UserRoles = new Collection<UserRole>() };
        
            var response = await _userService.CreateUserAsync(user, ApplicationRole.User);

            Assert.False(response.Success);
            Assert.Equal("Username already existed", response.Message);
        }

        [Fact]
        public async Task Should_Find_Existing_User_By_Username()
        {
            var user = await _userService.FindByUsernameAsync("ThisIsUserForTest");
            Assert.NotNull(user);
            Assert.Equal("ThisIsUserForTest", user.Username);
        }

        [Fact]
        public async Task Should_Return_Null_When_Trying_To_Find_User_By_Invalid_Username()
        {
            var user = await _userService.FindByUsernameAsync("ThisIsSecondUserForTest");
            Assert.Null(user);
        }
    }
}