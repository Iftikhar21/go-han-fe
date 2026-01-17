using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_han_fe.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RoleDTO Role { get; set; } = new();
    }

}