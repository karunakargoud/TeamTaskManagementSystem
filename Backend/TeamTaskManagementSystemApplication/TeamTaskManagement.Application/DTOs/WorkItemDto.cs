using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskManagement.Application.DTOs
{
    public class WorkItemDto
    {
        public int WorkItemId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Status { get; set; }

        public string Priority { get; set; } = "Medium";

        public DateTime? DueDate { get; set; }

        public int? AssignedToUserId { get; set; }

        public int? TeamId { get; set; }
    }
}
