using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Domain.Models.Users
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        
        public Guid RoleId { get;set; }
        public Role Role { get;set; }
    }
}