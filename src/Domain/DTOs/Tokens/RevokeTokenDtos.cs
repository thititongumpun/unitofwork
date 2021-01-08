using System.ComponentModel.DataAnnotations;

namespace src.Domain.DTOs.Tokens
{
    public class RevokeTokenDtos
    {
        [Required]
        public string Token { get; set; }
    }
}