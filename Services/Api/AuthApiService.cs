using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using go_han_fe.Models.DTOs;
using go_han_fe.Models.DTOs.Auth;
using go_han_fe.Models.DTOs.Common;

namespace go_han_fe.Services.Api
{
    public class AuthApiService
    {
        private readonly HttpClient _http;
        private readonly ApiSettings _settings;

        public AuthApiService(HttpClient http, ApiSettings settings){
            _http = http;
            _settings = settings;
        }

        public async Task<ApiResponse<AuthResponseDTO>?> LoginAsync(string email, string password)
        {
            var response = await _http.PostAsJsonAsync(
                _settings.BaseUrl + "auth/login",
                new { email, password }
            );

            var result = await response.Content
                .ReadFromJsonAsync<ApiResponse<AuthResponseDTO>>();

            return result;
        }


        public async Task<UserDTO?> MeAsync(string token)
        {
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer", token
                );

            var response = await _http.GetAsync(
                _settings.BaseUrl + "auth/me"
            );

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content
                .ReadFromJsonAsync<ApiResponse<UserDTO>>();

            return result?.Data;
        }

        public async Task<ApiResponse<UserDTO>?> RegisterAsync(string username, string email, string password)
        {
            var response = await _http.PostAsJsonAsync(
                _settings.BaseUrl + "auth/register",
                new
                {
                    username,
                    email,
                    passwordHash = password,
                    roleId = 2
                }
            );

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
                return null;

            return JsonSerializer.Deserialize<ApiResponse<UserDTO>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}