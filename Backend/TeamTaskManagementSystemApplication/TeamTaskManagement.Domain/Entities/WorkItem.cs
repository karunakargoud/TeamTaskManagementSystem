using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskManagement.Domain.Entities
{
    public class WorkItem
    {
        public int WorkItemId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public WorkItemStatus Status { get; set; } = WorkItemStatus.ToDo;

        public string Priority { get; set; } = "Medium";

        public DateTime? DueDate { get; set; }

        public int? AssignedToUserId { get; set; }

        public int? TeamId { get; set; }

        public User? AssignedToUser { get; set; }

        public Team? Team { get; set; }

        public ICollection<Comment> Comments { get; set; }
            = new List<Comment>();
    }
}
