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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;

        public CommentService(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CommentDto>> GetAllAsync()
        {
            var comments = await _repository.GetAllAsync();

            return comments.Select(MapToDto).ToList();
        }

        public async Task<CommentDto?> GetByIdAsync(int id)
        {
            var comment = await _repository.GetByIdAsync(id);

            return comment == null ? null : MapToDto(comment);
        }

        public async Task<CommentDto> CreateAsync(CommentDto dto)
        {
            var comment = new Comment
            {
                CommentText = dto.CommentText,
                WorkItemId = dto.WorkItemId,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _repository.CreateAsync(comment);

            return MapToDto(result);
        }

        public async Task<CommentDto?> UpdateAsync(
            int id,
            CommentDto dto)
        {
            var comment = new Comment
            {
                CommentText = dto.CommentText,
                WorkItemId = dto.WorkItemId,
                UserId = dto.UserId,
                CreatedAt = dto.CreatedAt
            };

            var result = await _repository.UpdateAsync(id, comment);

            return result == null ? null : MapToDto(result);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        private static CommentDto MapToDto(Comment comment)
        {
            return new CommentDto
            {
                CommentId = comment.CommentId,
                CommentText = comment.CommentText,
                WorkItemId = comment.WorkItemId,
                UserId = comment.UserId,
                CreatedAt = comment.CreatedAt
            };
        }
    }
}

