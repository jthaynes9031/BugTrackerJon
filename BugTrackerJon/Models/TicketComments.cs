using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Models
{
    public class TicketComments
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Body { get; set; }
        public string OwnerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Tickets Ticket { get; set; }
        public ApplicationUser Owner { get; set; }
    }
}