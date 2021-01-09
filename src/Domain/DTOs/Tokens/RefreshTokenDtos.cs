using System.ComponentModel.DataAnnotations;

namespace src.Domain.DTOs.Tokens
{
    public class RefreshTokenDtos
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [StringLength(25)]
        public string Username { get; set; }
    }
}