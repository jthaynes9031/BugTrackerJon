using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Models
{
    public class TicketStatus
    {
        public TicketStatus()
        {
            Tickets = new HashSet<Tickets>();
        }
        public int Id { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}