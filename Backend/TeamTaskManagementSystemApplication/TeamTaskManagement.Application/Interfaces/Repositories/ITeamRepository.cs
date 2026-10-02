using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManagement.Domain.Entities;

namespace TeamTaskManagement.Application.Interfaces.Repositories
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetAllAsync();

        Task<Team?> GetByIdAsync(int id);

        Task<Team> CreateAsync(Team team);

        Task<Team?> UpdateAsync(int id, Team team);

        Task<bool> DeleteAsync(int id);
    }
}
