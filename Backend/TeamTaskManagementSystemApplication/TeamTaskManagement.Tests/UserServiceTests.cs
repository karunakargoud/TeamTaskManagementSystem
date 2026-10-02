using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManagement.Application.Interfaces.Repositories;
using TeamTaskManagement.Application.Services;
using TeamTaskManagement.Domain.Entities;
using Xunit;

namespace TeamTaskManagement.Tests
{

    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _repositoryMock = new Mock<IUserRepository>();

            _service = new UserService(
                _repositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnUsers()
        {
            // Arrange
            var users = new List<User>
        {
            new User
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john@test.com",
                Role = "User"
            },
            new User
            {
                UserId = 2,
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@test.com",
                Role = "Admin"
            }
        };

            _repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(users);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_WhenUserExists_ShouldReturnUser()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john@test.com",
                Role = "User"
            };

            _repositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(user);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("john@test.com", result.Email);
        }

        [Fact]
        public async Task GetByIdAsync_WhenUserDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            _repositoryMock
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_WhenUserExists_ShouldReturnTrue()
        {
            // Arrange
            _repositoryMock
                .Setup(x => x.DeleteAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_WhenUserDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            _repositoryMock
                .Setup(x => x.DeleteAsync(999))
                .ReturnsAsync(false);

            // Act
            var result = await _service.DeleteAsync(999);

            // Assert
            Assert.False(result);
        }
    }
}
