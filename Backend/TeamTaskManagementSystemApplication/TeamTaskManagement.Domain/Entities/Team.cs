using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskManagement.Domain.Entities
{
    public class Team
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<WorkItem> WorkItems { get; set; }
            = new List<WorkItem>();
    }
}
