using BugTrackerJon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackerJon.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var dash = new Dashboard();
            dash.myprojects = db.Projects.ToList();
            dash.mytickets = db.Tickets.ToList();
            dash.myusers = db.Users.ToList();
            return View(dash);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DemoUser()
        {
            return View();
        }
    }
}