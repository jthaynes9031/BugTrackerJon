using BugTrackerJon.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Helpers
{
    public class TicketHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoles userRoles = new UserRoles();
        public int SetDefaultTicketStatus()
        {
            return db.TicketStatus.FirstOrDefault(ts => ts.StatusName == "Open").Id;
        }

        public List<Tickets> ListMyTickets()
        {
            var myTickets = new List<Tickets>();
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            //Step 1: Obtain my Role
            var myRole = userRoles.ListUserRoles(userId).FirstOrDefault();

            //Step 2 US that Role to build the appropriate set of Tickets
            if (myRole == "Admin")
            {
                myTickets.AddRange(db.Tickets);
            }
            else if (myRole == "Project_Manager")
            {
                myTickets.AddRange(user.Project.SelectMany(p => p.Tickets));
            }
            else if (myRole == "Developer")
            {
                myTickets.AddRange(db.Tickets.Where(t => t.AssignedToUserId == userId));
            }
            else if (myRole == "Submitter")
            {
                myTickets.AddRange(db.Tickets.Where(t => t.OwnerUserId == userId));
            }
            return myTickets;
        }
    }
}