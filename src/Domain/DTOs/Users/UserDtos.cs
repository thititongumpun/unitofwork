using System;
using System.Collections.Generic;

namespace src.Domain.DTOs.Users
{
    public class UserDtos
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}   