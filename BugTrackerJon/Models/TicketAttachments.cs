using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Models
{
    public class TicketAttachments
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public string UserId { get; set; }
        public int TicketId { get; set; }

        //Navigaion
        public virtual Tickets Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}