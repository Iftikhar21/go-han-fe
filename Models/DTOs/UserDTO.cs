using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_han_fe.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        private RoleDTO Role { get; set; } = null!;

    }
}