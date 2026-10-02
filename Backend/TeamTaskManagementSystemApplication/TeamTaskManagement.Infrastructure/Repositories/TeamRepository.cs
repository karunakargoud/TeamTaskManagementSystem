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
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Team>> GetAllAsync()
        {
            return await _context.Teams
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Team?> GetByIdAsync(int id)
        {
            return await _context.Teams
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TeamId == id);
        }

        public async Task<Team> CreateAsync(Team team)
        {
            _context.Teams.Add(team);

            await _context.SaveChangesAsync();

            return team;
        }

        public async Task<Team?> UpdateAsync(int id, Team team)
        {
            var existing = await _context.Teams
                .FirstOrDefaultAsync(x => x.TeamId == id);

            if (existing == null)
                return null;

            existing.TeamName = team.TeamName;
            existing.Description = team.Description;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Teams
                .FirstOrDefaultAsync(x => x.TeamId == id);

            if (existing == null)
                return false;

            _context.Teams.Remove(existing);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
