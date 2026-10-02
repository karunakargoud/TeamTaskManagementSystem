using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManagement.Application.DTOs;
using TeamTaskManagement.Application.Interfaces.Repositories;
using TeamTaskManagement.Application.Interfaces.Services;
using TeamTaskManagement.Domain.Entities;

namespace TeamTaskManagement.Application.Services
{

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<UserDto?> RegisterAsync(RegisterDto dto)
        {
            var users = await _userRepository.GetAllAsync();

            var existingUser = users.FirstOrDefault(
                x => x.Email.ToLower() == dto.Email.ToLower());

            if (existingUser != null)
                return null;

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };

            var result = await _userRepository.CreateAsync(user);

            return new UserDto
            {
                UserId = result.UserId,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                Role = result.Role
            };
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
        {
            var users = await _userRepository.GetAllAsync();

            var user = users.FirstOrDefault(
                x => x.Email.ToLower() == dto.Email.ToLower());

            if (user == null)
                return null;

            bool passwordValid =
                BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (!passwordValid)
                return null;

            var token = GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
            };
        }

        private string GenerateToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey!));

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.UserId.ToString()),

            new Claim(
                ClaimTypes.Email,
                user.Email),

            new Claim(
                ClaimTypes.Role,
                user.Role),

            new Claim(
                ClaimTypes.Name,
                $"{user.FirstName} {user.LastName}")
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
