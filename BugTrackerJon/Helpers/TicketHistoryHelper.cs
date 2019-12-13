using BugTrackerJon.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Helpers
{
    public class TicketHistoryHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void RecordHistoricalChanges(Tickets oldTicket, Tickets newTicket)
        {
            if (oldTicket.TicketStatusId != newTicket.TicketStatusId)
            {
                var newHistoryRecord = new TicketHistories
                {
                    Property = "TicketStatusId",
                    OldValue = oldTicket.TicketStatus.StatusName,
                    NewValue = newTicket.TicketStatus.StatusName,
                    UpdateDate = (DateTime)newTicket.Updated,
                    UpdaterId = HttpContext.Current.User.Identity.GetUserId(),
                    TicketId = newTicket.Id
                };
                db.TicketHistories.Add(newHistoryRecord);
            }
            if (oldTicket.TicketPriorityId != newTicket.TicketPriorityId)
            {
                var newHistoryRecord = new TicketHistories
                {
                    Property = "TicketPriorityId",
                    OldValue = oldTicket.TicketPriority.PriorityName,
                    NewValue = newTicket.TicketPriority.PriorityName,
                    UpdateDate = (DateTime)newTicket.Updated,
                    UpdaterId = HttpContext.Current.User.Identity.GetUserId(),
                    TicketId = newTicket.Id
                };
                db.TicketHistories.Add(newHistoryRecord);
            }

            if (oldTicket.OwnerUserId != newTicket.OwnerUserId)
            {
                var newHistoryRecord = new TicketHistories
                {
                    Property = "DeveloperId",
                    OldValue = oldTicket.OwnerUser == null ? "UnAssigned" : oldTicket.OwnerUser.LastName,
                    NewValue = newTicket.OwnerUser == null ? "UnAssigned" : newTicket.OwnerUser.LastName,
                    UpdateDate = (DateTime)newTicket.Updated,
                    UpdaterId = HttpContext.Current.User.Identity.GetUserId(),
                    TicketId = newTicket.Id
                };
            }


            db.SaveChanges();
        }



    }
}