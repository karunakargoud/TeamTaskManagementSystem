using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskManagement.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string CommentText { get; set; } = string.Empty;

        public int WorkItemId { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public WorkItem? WorkItem { get; set; }

        public User? User { get; set; }
    }
}

