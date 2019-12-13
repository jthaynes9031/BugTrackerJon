using BugTrackerJon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Helpers
{
    public class BadgeHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static string GetColorClass(int PriorityId)
        {
            var color = "";
            var ticketP = db.TicketPriorities;
            if (PriorityId == ticketP.FirstOrDefault(t => t.PriorityName == "Low").Id)
            {
                color = "btn btn-custon-rounded-two btn-success btn-xs";
            }
            if (PriorityId == ticketP.FirstOrDefault(t => t.PriorityName == "Medium").Id)
            {
                color = "btn btn-custon-rounded-two btn-warning btn-xs";
            }
            if (PriorityId == ticketP.FirstOrDefault(t => t.PriorityName == "High").Id)
            {
                color = "btn btn-custon-rounded-two btn-primary btn-xs";
            }
            if (PriorityId == ticketP.FirstOrDefault(t => t.PriorityName == "Very High").Id)
            {
                color = "btn btn-custon-rounded-two btn-info btn-xs";

            }
            if (PriorityId == ticketP.FirstOrDefault(t => t.PriorityName == "I NEED IT").Id)
            {
                color = "btn btn-custon-rounded-two btn-danger btn-xs";
            }

            return color;
        }
        public static string GetStatusColorClass(int statusId)
        {
            var color = "";
            var ticketS = db.TicketStatus;
            if (statusId == ticketS.FirstOrDefault(t => t.StatusName == "Archived").Id)
            {
                color = "btn btn-custon-rounded-two btn-success btn-xs";
            };
            if (statusId == ticketS.FirstOrDefault(t => t.StatusName == "Open").Id)
            {
                color = "btn btn-custon-rounded-two btn-info btn-xs";
            }
            if (statusId == ticketS.FirstOrDefault(t => t.StatusName == "Assigned").Id)
            {
                color = "btn btn-custon-rounded-two btn-danger btn-xs";
            }
            if (statusId == ticketS.FirstOrDefault(t => t.StatusName == "In Progress").Id)
            {
                color = "btn btn-custon-rounded-two btn-primary btn-xs";
            }
            if (statusId == ticketS.FirstOrDefault(t => t.StatusName == "Resolved").Id)
            {
                color = "btn btn-custon-rounded-two btn-warning btn-xs";
            }

            return color;
        }
    }
}
