using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace go_han_fe.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username harus diisi")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email harus diisi")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password harus diisi")]
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; } = 3;
    }
}