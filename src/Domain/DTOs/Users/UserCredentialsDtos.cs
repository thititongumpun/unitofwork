using System.ComponentModel.DataAnnotations;

namespace src.Domain.DTOs.Users
{
    public class UserCredentialsDtos
    {
        [Required]
        [StringLength(25)]
        public string Username { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }
    }
}