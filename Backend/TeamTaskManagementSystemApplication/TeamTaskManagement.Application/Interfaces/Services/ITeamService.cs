using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManagement.Application.DTOs;

namespace TeamTaskManagement.Application.Interfaces.Services
{
    public interface ITeamService
    {
        Task<List<TeamDto>> GetAllAsync();

        Task<TeamDto?> GetByIdAsync(int id);

        Task<TeamDto> CreateAsync(TeamDto dto);

        Task<TeamDto?> UpdateAsync(int id, TeamDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
