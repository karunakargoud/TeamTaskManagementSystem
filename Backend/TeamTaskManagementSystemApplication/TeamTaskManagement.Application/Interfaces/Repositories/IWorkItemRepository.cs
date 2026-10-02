using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManagement.Domain.Entities;

namespace TeamTaskManagement.Application.Interfaces.Repositories
{
    public interface IWorkItemRepository
    {
        Task<List<WorkItem>> GetAllAsync();

        Task<WorkItem?> GetByIdAsync(int id);

        Task<WorkItem> CreateAsync(WorkItem workItem);

        Task<WorkItem?> UpdateAsync(int id, WorkItem workItem);

        Task<bool> DeleteAsync(int id);
    }
}
