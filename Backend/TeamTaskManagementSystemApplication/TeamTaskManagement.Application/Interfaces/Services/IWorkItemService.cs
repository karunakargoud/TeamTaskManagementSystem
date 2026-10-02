using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManagement.Application.DTOs;

namespace TeamTaskManagement.Application.Interfaces.Services
{
    public interface IWorkItemService
    {
        Task<List<WorkItemDto>> GetAllAsync();

        Task<WorkItemDto?> GetByIdAsync(int id);

        Task<WorkItemDto> CreateAsync(WorkItemDto dto);

        Task<WorkItemDto?> UpdateAsync(int id, WorkItemDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
