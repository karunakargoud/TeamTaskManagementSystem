using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManagement.Application.DTOs;

namespace TeamTaskManagement.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();

        Task<UserDto?> GetByIdAsync(int id);

        Task<UserDto> CreateAsync(UserDto dto);

        Task<UserDto?> UpdateAsync(int id, UserDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
