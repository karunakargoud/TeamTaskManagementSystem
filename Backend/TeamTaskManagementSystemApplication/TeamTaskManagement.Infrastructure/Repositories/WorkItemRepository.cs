using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManagement.Application.Interfaces.Repositories;
using TeamTaskManagement.Domain.Entities;
using TeamTaskManagement.Infrastructure.Data;

namespace TeamTaskManagement.Infrastructure.Repositories
{
    public class WorkItemRepository : IWorkItemRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkItem>> GetAllAsync()
        {
            return await _context.WorkItems
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<WorkItem?> GetByIdAsync(int id)
        {
            return await _context.WorkItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.WorkItemId == id);
        }

        public async Task<WorkItem> CreateAsync(WorkItem workItem)
        {
            _context.WorkItems.Add(workItem);

            await _context.SaveChangesAsync();

            return workItem;
        }

        public async Task<WorkItem?> UpdateAsync(
            int id,
            WorkItem workItem)
        {
            var existing = await _context.WorkItems
                .FirstOrDefaultAsync(x => x.WorkItemId == id);

            if (existing == null)
                return null;

            existing.Title = workItem.Title;
            existing.Description = workItem.Description;
            existing.Status = workItem.Status;
            existing.Priority = workItem.Priority;
            existing.DueDate = workItem.DueDate;
            existing.AssignedToUserId = workItem.AssignedToUserId;
            existing.TeamId = workItem.TeamId;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.WorkItems
                .FirstOrDefaultAsync(x => x.WorkItemId == id);

            if (existing == null)
                return false;

            _context.WorkItems.Remove(existing);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

