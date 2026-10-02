using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManagement.Application.DTOs;
using TeamTaskManagement.Application.Interfaces.Repositories;
using TeamTaskManagement.Application.Services;
using TeamTaskManagement.Domain.Entities;



namespace TeamTaskManagement.Tests
{
    public class CommentServiceTests
    {
        private readonly Mock<ICommentRepository> _repositoryMock;
        private readonly CommentService _service;

        public CommentServiceTests()
        {
            _repositoryMock = new Mock<ICommentRepository>();
            _service = new CommentService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnComments()
        {
            // Arrange
            var comments = new List<Comment>
        {
            new Comment
            {
                CommentId = 1,
                CommentText = "Task started",
                WorkItemId = 1,
                UserId = 1
            },
            new Comment
            {
                CommentId = 2,
                CommentText = "Task completed",
                WorkItemId = 1,
                UserId = 2
            }
        };

            _repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(comments);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateComment()
        {
            // Arrange
            var dto = new CommentDto
            {
                CommentText = "Task started",
                WorkItemId = 1,
                UserId = 1
            };

            var comment = new Comment
            {
                CommentId = 1,
                CommentText = "Task started",
                WorkItemId = 1,
                UserId = 1
            };

            _repositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Comment>()))
                .ReturnsAsync(comment);

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CommentId);
            Assert.Equal("Task started", result.CommentText);
        }
    }
}
