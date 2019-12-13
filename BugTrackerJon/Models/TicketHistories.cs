using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Models
{
    public class TicketHistories
    {
        public int Id { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public int TicketId { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string UpdaterId { get; set; }
        
        public virtual Tickets Ticket { get; set; }
        public virtual ApplicationUser Updater { get; set; }
    }
}