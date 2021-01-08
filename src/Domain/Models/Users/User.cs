using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace src.Domain.Models.Users
{
    public class User : Entity
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        public ICollection<UserRole> UserRoles { get; set; } = new Collection<UserRole>();
    }
}