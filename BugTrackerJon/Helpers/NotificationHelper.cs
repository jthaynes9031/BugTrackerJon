using BugTrackerJon.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BugTrackerJon.Helpers
{
    public class NotificationHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public void ManageNotifications(Tickets oldTicket, Tickets newTicket)
        {
            var ticketHasBeenAssigned = oldTicket.AssignedToUserId == null && newTicket.AssignedToUserId != null;
            var ticketHasBeenUnAssigned = oldTicket.AssignedToUserId != null && newTicket.AssignedToUserId == null;
            var ticketHasBeenReassigned = oldTicket.AssignedToUserId != null && newTicket.AssignedToUserId != null && oldTicket.AssignedToUserId != newTicket.AssignedToUserId;

            if (ticketHasBeenAssigned)
                AddAssignmentNotification(oldTicket, newTicket);
            else if (ticketHasBeenUnAssigned)
                AddUnassignmentNotification(oldTicket, newTicket);
            else if (ticketHasBeenReassigned)
                AddAssignmentNotification(oldTicket, newTicket);
            AddUnassignmentNotification(oldTicket, newTicket);

        }
        private void AddAssignmentNotification(Tickets oldTicket, Tickets newTicket)
        {
            var notification = new TicketNotifications
            {
                TicketId = newTicket.Id,
                IsRead = false,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                RecipientId = newTicket.AssignedToUserId,
                CreatedDate = DateTime.Now,
                NotificationBody = $"You have been assigned to a ticket Id {newTicket.Id} on project {newTicket.Project.Name}. The ticket title is {newTicket.Title}"
            };
            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }
        private void AddUnassignmentNotification(Tickets oldTicket, Tickets newTicket)
        {
            //var notification = new TicketNotifications
            //{
            //    TicketId = newTicket.Id,
            //    IsRead = false,
            //    SenderId = HttpContext.Current.User.Identity.GetUserId(),
            //    RecipientId = newTicket.AssignedToUserId,
            //    CreatedDate = DateTime.Now,
            //    NotificationBody = $"You have been removed from Ticket {newTicket.Id} On project {newTicket.Project.Name}"
            //};
            //db.TicketNotifications.Remove(notification);
            //db.SaveChanges();
        }
        public static List<TicketNotifications> GetUnreadNotifications()
        {
            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            var ticketNotifications = db.TicketNotifications.Where(t => t.RecipientId == currentUserId && t.IsRead == false).ToList();
            return ticketNotifications;
        }
    }
}