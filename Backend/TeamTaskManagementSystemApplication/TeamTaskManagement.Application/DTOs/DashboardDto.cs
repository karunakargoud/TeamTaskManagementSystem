using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskManagement.Application.DTOs
{
    public class DashboardDto
    {
        public int TotalTasks { get; set; }

        public int ToDo { get; set; }

        public int InProgress { get; set; }

        public int Done { get; set; }

        public int HighPriority { get; set; }

        public int Overdue { get; set; }
    }
}
