using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public string AssignedUserId { get; set; }
        public virtual ApplicationUser AssignedUser { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<ProjectUser> Project { get; set; }
        public Projects()
        {
            Tickets = new HashSet<Tickets>();
            Project = new HashSet<ProjectUser>();
            Users = new HashSet<ApplicationUser>();
        }
    }
}