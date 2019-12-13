using BugTrackerJon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackerJon.Controllers
{
    public class GraphingController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Graphing
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ProduceChart1Data()
        {
            var myData = new List<MorrisBarCharts>();
            MorrisBarCharts data = null;
            foreach (var priority in db.TicketPriorities.ToList())
            {
                data = new MorrisBarCharts();
                data.label = priority.PriorityName;
                data.value = db.Tickets.Where(t => t.TicketPriority.PriorityName == priority.PriorityName).Count();
                myData.Add(data);
            }
            return Json(myData);
        }

        public JsonResult ProduceBarChart()
        {
            BarChartData myData = new BarChartData();
            myData.Labels = new List<string> { "Low", "Medium", "High", "Urgent" };
            BCDataSet bCData = new BCDataSet();
            bCData.Label = "Ticket Priorities";
            List<int> pIds = new List<int>();
            foreach (var priority in db.TicketPriorities.ToList())
            {
                try
                {

                    int count = db.Tickets.Where(t => t.TicketPriority.PriorityName == priority.PriorityName).Count();
                    pIds.Add(count);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            bCData.Data = pIds;
            bCData.BackgroundColor = new List<string>
            {
                    "rgba(255, 99, 132, 0.2)",
                    "rgba(54, 162, 235, 0.2)",
                    "rgba(255, 206, 86, 0.2)",
                    "rgba(75, 192, 192, 0.2)",
                    "rgba(255, 159, 64, 0.2)"

            };
            bCData.BorderColor = new List<string>
            {
                    "rgba(255,99,132,1)",
                    "rgba(54, 162, 235, 1)",
                    "rgba(255, 206, 86, 1)",
                    "rgba(75, 192, 192, 1)",
                    "rgba(153, 102, 255, 1)"
            };
            bCData.BorderWidth = 1;
            List<BCDataSet> dataSet = new List<BCDataSet>();
            dataSet.Add(bCData);
            myData.DataSets = dataSet;
            return Json(myData);

        }

        public JsonResult ProduceChart2Data()
        {
            var myData = new List<MorrisBarCharts>();
            MorrisBarCharts data = null;
            foreach (var status in db.TicketStatus.ToList())
            {
                myData.Add(new MorrisBarCharts
                {
                    label = status.StatusName,
                    value = db.Tickets.Where(t => t.TicketStatus.StatusName
                    == status.StatusName).Count()
                });
            }
            return Json(myData);
        }

    }


}