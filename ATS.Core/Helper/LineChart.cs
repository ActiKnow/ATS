using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Core.Helper
{
   public class LineChart
    {
        public LineChart()
        {
            dataPoints = new List<DataPoints>();
        }
       public string type { get; set; }
       public bool showInLegend{ get; set; }
       public string legendText { get; set; }      
       public List<DataPoints> dataPoints { get; set; }
    }

    public class DataPoints
    {
        public decimal y { get; set; }
        public string label { get; set; }
    }
}
