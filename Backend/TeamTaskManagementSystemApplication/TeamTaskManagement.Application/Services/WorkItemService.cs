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
    public class WorkItemService : IWorkItemService
    {
        private readonly IWorkItemRepository _repository;
        private readonly INotificationService _notificationService;


        public WorkItemService(IWorkItemRepository repository, INotificationService notificationService)
        {
            _repository = repository;
            _notificationService = notificationService;
        }

        public async Task<List<WorkItemDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();

            return items.Select(MapToDto).ToList();
        }

        public async Task<WorkItemDto?> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);

            return item == null ? null : MapToDto(item);
        }

        public async Task<WorkItemDto> CreateAsync(
            WorkItemDto dto)
        {
            var item = new WorkItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = (WorkItemStatus)dto.Status,
                Priority = dto.Priority,
                DueDate = dto.DueDate,
                AssignedToUserId = dto.AssignedToUserId,
                TeamId = dto.TeamId
            };

            var result = await _repository.CreateAsync(item);
            // Send notification when task is assigned
            if (result.AssignedToUserId.HasValue)
            {
                await _notificationService.SendNotificationAsync(
                    result.AssignedToUserId.Value,
                    $"New task assigned to you: {result.Title}");
            }

            return MapToDto(result);
        }

        public async Task<WorkItemDto?> UpdateAsync(
            int id,
            WorkItemDto dto)
        {
            var item = new WorkItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = (WorkItemStatus)dto.Status,
                Priority = dto.Priority,
                DueDate = dto.DueDate,
                AssignedToUserId = dto.AssignedToUserId,
                TeamId = dto.TeamId
            };

            var result = await _repository.UpdateAsync(id, item);

            return result == null ? null : MapToDto(result);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        private static WorkItemDto MapToDto(WorkItem item)
        {
            return new WorkItemDto
            {
                WorkItemId = item.WorkItemId,
                Title = item.Title,
                Description = item.Description,
                Status = (int)item.Status,
                Priority = item.Priority,
                DueDate = item.DueDate,
                AssignedToUserId = item.AssignedToUserId,
                TeamId = item.TeamId
            };
        }
    }
}
