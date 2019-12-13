using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Models
{
    public class ProjectUser
    {
        public int Id { get; set; }
        public int AssignedProjectId { get; set; }
        public string AssignedUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }

        public virtual ApplicationUser AssignedUser {get;set;}
        public virtual Projects AssignedProject { get; set; }



    }
}