using src.Domain.Models.Users;

namespace src.Domain.ViewModels.Users
{
    public class UserResponse : BaseResponse
    {
        public User User { get; private set; }
        public UserResponse(bool success, string message, User user): base(success, message) 
        {
            User = user;
        }
    }
}