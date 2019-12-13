using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Models
{
    public class TicketType
    {
        public TicketType()
        {
            Tickets = new HashSet<Tickets>();
        }
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        //Navigation
        public virtual ICollection<Tickets>Tickets { get; set; }
    }
}