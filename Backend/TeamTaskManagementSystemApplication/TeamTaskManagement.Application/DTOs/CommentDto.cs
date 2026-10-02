using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskManagement.Application.DTOs
{
    public class CommentDto
    {
        public int CommentId { get; set; }

        public string CommentText { get; set; } = string.Empty;

        public int WorkItemId { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
