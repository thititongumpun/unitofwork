using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace src.Domain.Models.Users
{
    public class Role : Entity
    {
        [Required]
        [StringLength(25)]
        public string Name { get;set; }
        public ICollection<UserRole> UserRoles { get; set; } = new Collection<UserRole>();
    }
}