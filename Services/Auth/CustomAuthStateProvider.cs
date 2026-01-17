using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using go_han_fe.Services.State;
using Microsoft.AspNetCore.Components.Authorization;

namespace go_han_fe.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly AuthState _authState;

        public CustomAuthStateProvider(AuthState authState)
        {
            _authState = authState;

            // Subscribe to auth state changes
            _authState.OnChange += AuthStateChanged;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;

            if (_authState.IsAuthenticated && _authState.User != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _authState.User.Username),
                    new Claim(ClaimTypes.Email, _authState.User.Email),
                    new Claim(ClaimTypes.NameIdentifier, _authState.User.Id.ToString()),
                    new Claim(ClaimTypes.Role, _authState.User.Role?.RoleName ?? "User")
                };

                identity = new ClaimsIdentity(claims, "CustomAuth");
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }

        private void AuthStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void Dispose()
        {
            _authState.OnChange -= AuthStateChanged;
        }
    }
}