using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSDiary.Models
{
    public class Gráfico
    {
        public class ChartData
        {
            public string title { set; get; }
            public Axis axisX { set; get; }
            public Axis axisY { set; get; }
            public Data data { set; get; }
        }

        public class Axis
        {
            public string title { set; get; }
            public string titleFontColor { set; get; }
        }
        public class Data
        {
            public string color { set; get; }
            public List<DataPoint> data { set; get; }
        }

        public class DataPoint
        {
            public int x { set; get; }
            public decimal y { set; get; }
        }
    }
}