﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Models
{
    public class TicketPriorities
    {
        public TicketPriorities()
        {
            Tickets = new HashSet<Tickets>();
        }
        public int Id { get; set; }
        public string PriorityName { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}