using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManagement.Application.DTOs;
using TeamTaskManagement.Application.Interfaces.Repositories;
using TeamTaskManagement.Application.Interfaces.Services;
using TeamTaskManagement.Domain.Entities;

namespace TeamTaskManagement.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IWorkItemRepository _workItemRepository;

        public DashboardService(IWorkItemRepository workItemRepository)
        {
            _workItemRepository = workItemRepository;
        }

        public async Task<DashboardDto> GetDashboardAsync()
        {
            var tasks = await _workItemRepository.GetAllAsync();

            var dashboard = new DashboardDto
            {
                TotalTasks = tasks.Count,

                ToDo = tasks.Count(x =>
                    x.Status == WorkItemStatus.ToDo),

                InProgress = tasks.Count(x =>
                    x.Status == WorkItemStatus.InProgress),

                Done = tasks.Count(x =>
                    x.Status == WorkItemStatus.Done),

                HighPriority = tasks.Count(x =>
                    x.Priority == "High"),

                Overdue = tasks.Count(x =>
                    x.DueDate.HasValue &&
                    x.DueDate.Value.Date < DateTime.UtcNow.Date &&
                    x.Status != WorkItemStatus.Done)
            };

            return dashboard;
        }
    }
}
