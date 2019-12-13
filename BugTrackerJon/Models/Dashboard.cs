using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Models
{
    public class Dashboard
    {
        public virtual ICollection<Projects> myprojects { get; set; }
        public virtual ICollection<Tickets> mytickets { get; set; }
        public virtual ICollection<ApplicationUser> myusers { get; set; }
        public Dashboard()
        {
            myprojects = new HashSet<Projects>();
            mytickets = new HashSet<Tickets>();
            myusers = new HashSet<ApplicationUser>();
        }

    }
}