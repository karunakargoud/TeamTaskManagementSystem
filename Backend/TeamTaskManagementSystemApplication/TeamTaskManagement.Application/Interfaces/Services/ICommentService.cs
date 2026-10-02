using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManagement.Application.DTOs;

namespace TeamTaskManagement.Application.Interfaces.Services
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllAsync();

        Task<CommentDto?> GetByIdAsync(int id);

        Task<CommentDto> CreateAsync(CommentDto dto);

        Task<CommentDto?> UpdateAsync(int id, CommentDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
