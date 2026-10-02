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
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repository;

        public TeamService(ITeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TeamDto>> GetAllAsync()
        {
            var teams = await _repository.GetAllAsync();

            return teams.Select(MapToDto).ToList();
        }

        public async Task<TeamDto?> GetByIdAsync(int id)
        {
            var team = await _repository.GetByIdAsync(id);

            return team == null ? null : MapToDto(team);
        }

        public async Task<TeamDto> CreateAsync(TeamDto dto)
        {
            var team = new Team
            {
                TeamName = dto.TeamName,
                Description = dto.Description
            };

            var result = await _repository.CreateAsync(team);

            return MapToDto(result);
        }

        public async Task<TeamDto?> UpdateAsync(
            int id,
            TeamDto dto)
        {
            var team = new Team
            {
                TeamName = dto.TeamName,
                Description = dto.Description
            };

            var result = await _repository.UpdateAsync(id, team);

            return result == null ? null : MapToDto(result);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        private static TeamDto MapToDto(Team team)
        {
            return new TeamDto
            {
                TeamId = team.TeamId,
                TeamName = team.TeamName,
                Description = team.Description
            };
        }
    }
}

