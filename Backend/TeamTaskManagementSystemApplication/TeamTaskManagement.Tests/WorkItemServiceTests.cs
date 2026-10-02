using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManagement.Application.DTOs;
using TeamTaskManagement.Application.Interfaces.Repositories;
using TeamTaskManagement.Application.Interfaces.Services;
using TeamTaskManagement.Application.Services;
using TeamTaskManagement.Domain.Entities;

namespace TeamTaskManagement.Tests
{

    public class WorkItemServiceTests
    {
        private readonly Mock<IWorkItemRepository> _repositoryMock;
        private readonly Mock<INotificationService> _notificationMock;
        private readonly WorkItemService _service;

        public WorkItemServiceTests()
        {
            _repositoryMock = new Mock<IWorkItemRepository>();
            _notificationMock = new Mock<INotificationService>();

            _service = new WorkItemService(
                _repositoryMock.Object,
                _notificationMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnWorkItems()
        {
            // Arrange
            var workItems = new List<WorkItem>
        {
            new WorkItem
            {
                WorkItemId = 1,
                Title = "Create Login API",
                Description = "Create JWT login API",
                Status = WorkItemStatus.ToDo,
                Priority = "High"
            },
            new WorkItem
            {
                WorkItemId = 2,
                Title = "Create Dashboard",
                Description = "Create dashboard",
                Status = WorkItemStatus.InProgress,
                Priority = "Medium"
            }
        };

            _repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(workItems);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_WhenItemExists_ShouldReturnItem()
        {
            // Arrange
            var item = new WorkItem
            {
                WorkItemId = 1,
                Title = "Create Login API",
                Description = "Create JWT login API",
                Status = WorkItemStatus.ToDo,
                Priority = "High"
            };

            _repositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(item);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Create Login API", result.Title);
            Assert.Equal(1, result.Status);
        }

        [Fact]
        public async Task GetByIdAsync_WhenItemDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            _repositoryMock
                .Setup(x => x.GetByIdAsync(100))
                .ReturnsAsync((WorkItem?)null);

            // Act
            var result = await _service.GetByIdAsync(100);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateWorkItem()
        {
            // Arrange
            var dto = new WorkItemDto
            {
                Title = "Create Login API",
                Description = "Create JWT login API",
                Status = 1,
                Priority = "High"
            };

            var workItem = new WorkItem
            {
                WorkItemId = 1,
                Title = "Create Login API",
                Description = "Create JWT login API",
                Status = WorkItemStatus.ToDo,
                Priority = "High"
            };

            _repositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<WorkItem>()))
                .ReturnsAsync(workItem);

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.WorkItemId);
            Assert.Equal("Create Login API", result.Title);
        }

        [Fact]
        public async Task DeleteAsync_WhenItemExists_ShouldReturnTrue()
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
        public async Task DeleteAsync_WhenItemDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            _repositoryMock
                .Setup(x => x.DeleteAsync(100))
                .ReturnsAsync(false);

            // Act
            var result = await _service.DeleteAsync(100);

            // Assert
            Assert.False(result);
        }
    }
}
