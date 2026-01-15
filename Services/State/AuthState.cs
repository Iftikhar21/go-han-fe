using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han_fe.Models.DTOs;

namespace go_han_fe.Services.State
{
    public class AuthState
    {
        public string? Token { get; private set; }
        public UserDTO? User { get; private set; }

        public bool IsAuthenticated => !string.IsNullOrEmpty(Token);

        public void SetAuth(string token, UserDTO user)
        {
            Token = token;
            User = user;
        }

        public void Logout()
        {
            Token = null;
            User = null;
        }
    }
}