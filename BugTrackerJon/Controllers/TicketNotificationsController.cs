using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTrackerJon.Models;

namespace BugTrackerJon.Controllers
{
    public class TicketNotificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Dismiss(int id)
        {
            var notification = db.TicketNotifications.Find(id);
            notification.IsRead = true;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


        // GET: TicketNotifications
        public ActionResult Index()
        {
            var ticketNotifications = db.TicketNotifications.Include(t => t.Recipient).Include(t => t.Ticket);
            return View(ticketNotifications.ToList());
        }

        // GET: TicketNotifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotifications ticketNotifications = db.TicketNotifications.Find(id);
            if (ticketNotifications == null)
            {
                return HttpNotFound();
            }
            return View(ticketNotifications);
        }

        // GET: TicketNotifications/Create
        public ActionResult Create()
        {
            ViewBag.RecipientId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "OwnerUserId");
            return View();
        }

        // POST: TicketNotifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TicketId,NotificationBody,CreatedDate,RecipientId,IsRead")] TicketNotifications ticketNotifications)
        {
            if (ModelState.IsValid)
            {
                db.TicketNotifications.Add(ticketNotifications);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RecipientId = new SelectList(db.Users, "Id", "FirstName", ticketNotifications.RecipientId);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "OwnerUserId", ticketNotifications.TicketId);
            return View(ticketNotifications);
        }

        // GET: TicketNotifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotifications ticketNotifications = db.TicketNotifications.Find(id);
            if (ticketNotifications == null)
            {
                return HttpNotFound();
            }
            ViewBag.RecipientId = new SelectList(db.Users, "Id", "FirstName", ticketNotifications.RecipientId);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "OwnerUserId", ticketNotifications.TicketId);
            return View(ticketNotifications);
        }

        // POST: TicketNotifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,NotificationBody,CreatedDate,RecipientId,IsRead")] TicketNotifications ticketNotifications)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketNotifications).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RecipientId = new SelectList(db.Users, "Id", "FirstName", ticketNotifications.RecipientId);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "OwnerUserId", ticketNotifications.TicketId);
            return View(ticketNotifications);
        }

        // GET: TicketNotifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotifications ticketNotifications = db.TicketNotifications.Find(id);
            if (ticketNotifications == null)
            {
                return HttpNotFound();
            }
            return View(ticketNotifications);
        }

        // POST: TicketNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketNotifications ticketNotifications = db.TicketNotifications.Find(id);
            db.TicketNotifications.Remove(ticketNotifications);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
