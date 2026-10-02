using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TeamTaskManagement.Application.Interfaces.Repositories;
using TeamTaskManagement.Application.Services;
using TeamTaskManagement.Domain.Entities;
using Xunit;


namespace TeamTaskManagement.Tests
{

    public class DashboardServiceTests
    {
        private readonly Mock<IWorkItemRepository> _repositoryMock;
        private readonly DashboardService _service;

        public DashboardServiceTests()
        {
            _repositoryMock = new Mock<IWorkItemRepository>();
            _service = new DashboardService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetDashboardAsync_ShouldReturnCorrectCounts()
        {
            // Arrange
            var tasks = new List<WorkItem>
        {
            new WorkItem
            {
                WorkItemId = 1,
                Status = WorkItemStatus.ToDo,
                Priority = "High"
            },
            new WorkItem
            {
                WorkItemId = 2,
                Status = WorkItemStatus.InProgress,
                Priority = "Medium"
            },
            new WorkItem
            {
                WorkItemId = 3,
                Status = WorkItemStatus.Done,
                Priority = "High"
            }
        };

            _repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(tasks);

            // Act
            var result = await _service.GetDashboardAsync();

            // Assert
            Assert.Equal(3, result.TotalTasks);
            Assert.Equal(1, result.ToDo);
            Assert.Equal(1, result.InProgress);
            Assert.Equal(1, result.Done);
            Assert.Equal(2, result.HighPriority);
        }
    }
}
