// Services/State/AuthState.cs
using go_han_fe.Models.DTOs;
using System;

namespace go_han_fe.Services.State
{
    public class AuthState
    {
        public string? Token { get; private set; }
        public UserDTO? User { get; private set; }

        public bool IsAuthenticated => !string.IsNullOrEmpty(Token);
        public bool IsLoaded { get; private set; }

        // Event untuk notifikasi perubahan state
        public event Action? OnChange;

        // Method untuk cek role
        public bool IsAdmin() => User?.Role?.Id == 1;
        public bool IsEmployee() => User?.Role?.Id == 2;

        // Property helper
        public string? UserName => User?.Username;
        public string RoleName() => User?.Role?.RoleName ?? "-";

        /// <summary>
        /// Set authentication state dengan token dan user data
        /// </summary>
        public void SetAuth(string token, UserDTO user)
        {
            Console.WriteLine("=== SET AUTH CALLED ===");
            Console.WriteLine($"Token: {(string.IsNullOrEmpty(token) ? "NULL" : "EXISTS")}");
            Console.WriteLine($"User: {(user == null ? "NULL" : "EXISTS")}");

            Token = token;
            User = user;
            IsLoaded = true;

            

            if (user?.Role != null)
            {
                Console.WriteLine($"User ID: {user.Id}");
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"Role ID: {user.Role.Id}");
                Console.WriteLine($"Role Name: {user.Role.RoleName}");
                Console.WriteLine($"IsAdmin(): {IsAdmin()}");
                Console.WriteLine($"IsEmployee(): {IsEmployee()}");
            }
            else
            {
                Console.WriteLine("⚠️ WARNING: User or Role is NULL!");
            }

            Console.WriteLine("Notifying state change...");
            NotifyStateChanged();
        }
        

        /// <summary>
        /// Clear authentication state
        /// </summary>
        public void Logout()
        {
            Console.WriteLine("=== LOGOUT CALLED ===");
            Token = null;
            User = null;
            IsLoaded = false;
            NotifyStateChanged();
        }

        /// <summary>
        /// Mark state as loaded (for initial load)
        /// </summary>
        public void MarkLoaded()
        {
            IsLoaded = true;
            NotifyStateChanged();
        }

        /// <summary>
        /// Notify subscribers about state changes
        /// </summary>
        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public Task WaitUntilLoadedAsync()
        {
            if (IsLoaded)
                return Task.CompletedTask;

            var tcs = new TaskCompletionSource();
            OnLoaded += () => tcs.TrySetResult();
            return tcs.Task;
        }

        public event Action? OnLoaded;

    }
}