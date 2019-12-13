using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerJon.Models
{
    public class BarChartData
    {
        public List<string> Labels { get; set; }
        public List<BCDataSet> DataSets { get; set; }
    }
    public class BCDataSet
    {
        public string Label { get; set; }
        public List<int> Data { get; set; }
        public List<string> BackgroundColor { get; set; }
        public List<string> BorderColor { get; set; }
        public int BorderWidth { get; set; }
    }

    
}