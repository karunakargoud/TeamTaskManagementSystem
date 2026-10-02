using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using TeamTaskManagement.Application.DTOs;
using TeamTaskManagement.Application.Interfaces.Repositories;
using TeamTaskManagement.Application.Services;
using TeamTaskManagement.Domain.Entities;
using Xunit;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TeamTaskManagement.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthService _service;

        public AuthServiceTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();

            _configurationMock
                .Setup(x => x["Jwt:Key"])
                .Returns("TeamTaskManagementSecretKey12345678901234567890");

            _configurationMock
                .Setup(x => x["Jwt:Issuer"])
                .Returns("TeamTaskManagementAPI");

            _configurationMock
                .Setup(x => x["Jwt:Audience"])
                .Returns("TeamTaskManagementClient");

            _service = new AuthService(
                _repositoryMock.Object,
                _configurationMock.Object);
        }

        [Fact]
        public async Task RegisterAsync_WhenEmailDoesNotExist_ShouldCreateUser()
        {
            // Arrange
            _repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<User>());

            var dto = new RegisterDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@test.com",
                Password = "Password123",
                Role = "User"
            };

            var createdUser = new User
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john@test.com",
                Password = "hashed-password",
                Role = "User"
            };

            _repositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(createdUser);

            // Act
            var result = await _service.RegisterAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
            Assert.Equal("john@test.com", result.Email);

            _repositoryMock.Verify(
                x => x.CreateAsync(It.IsAny<User>()),
                Times.Once);
        }

        [Fact]
        public async Task RegisterAsync_WhenEmailAlreadyExists_ShouldReturnNull()
        {
            // Arrange
            var existingUser = new User
            {
                UserId = 1,
                Email = "john@test.com"
            };

            _repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<User>
                {
                existingUser
                });

            var dto = new RegisterDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@test.com",
                Password = "Password123",
                Role = "User"
            };

            // Act
            var result = await _service.RegisterAsync(dto);

            // Assert
            Assert.Null(result);

            _repositoryMock.Verify(
                x => x.CreateAsync(It.IsAny<User>()),
                Times.Never);
        }

        [Fact]
        public async Task LoginAsync_WithValidCredentials_ShouldReturnToken()
        {
            // Arrange
            var password = "Password123";

            var user = new User
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john@test.com",
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Role = "User"
            };

            _repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<User>
                {
                user
                });

            var dto = new LoginDto
            {
                Email = "john@test.com",
                Password = password
            };

            // Act
            var result = await _service.LoginAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result.Token));
        }

        [Fact]
        public async Task LoginAsync_WhenUserDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            _repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<User>());

            var dto = new LoginDto
            {
                Email = "unknown@test.com",
                Password = "Password123"
            };

            // Act
            var result = await _service.LoginAsync(dto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task LoginAsync_WithInvalidPassword_ShouldReturnNull()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john@test.com",
                Password = BCrypt.Net.BCrypt.HashPassword("CorrectPassword"),
                Role = "User"
            };

            _repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<User>
                {
                user
                });

            var dto = new LoginDto
            {
                Email = "john@test.com",
                Password = "WrongPassword"
            };

            // Act
            var result = await _service.LoginAsync(dto);

            // Assert
            Assert.Null(result);
        }
    }
}
