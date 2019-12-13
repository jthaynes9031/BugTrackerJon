using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Models
{
    public class ProjectViewModel
    {
        public Projects Projects { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<Tickets> Tickets { get; set; }

        public ProjectViewModel()
        {
            Tickets = new HashSet<Tickets>();
            Users = new HashSet<ApplicationUser>();
        }
    }
}