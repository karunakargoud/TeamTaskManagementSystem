using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskManagement.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public ICollection<WorkItem> AssignedWorkItems { get; set; }
            = new List<WorkItem>();

        public ICollection<Comment> Comments { get; set; }
            = new List<Comment>();
    }
}
