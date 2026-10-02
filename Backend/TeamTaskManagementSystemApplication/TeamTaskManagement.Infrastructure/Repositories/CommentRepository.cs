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

    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CommentId == id);
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            _context.Comments.Add(comment);

            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment?> UpdateAsync(
            int id,
            Comment comment)
        {
            var existing = await _context.Comments
                .FirstOrDefaultAsync(x => x.CommentId == id);

            if (existing == null)
                return null;

            existing.CommentText = comment.CommentText;
            existing.WorkItemId = comment.WorkItemId;
            existing.UserId = comment.UserId;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Comments
                .FirstOrDefaultAsync(x => x.CommentId == id);

            if (existing == null)
                return false;

            _context.Comments.Remove(existing);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

