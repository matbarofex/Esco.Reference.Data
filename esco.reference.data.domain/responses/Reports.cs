using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Reports
    {
        public List<Report> fields { get; set; }
    }

    public class Report 
    {
        public string name { get; set; }
        public string label { get; set; }
        public string specName { get; set; }
        public bool? structural { get; set; }
        public string valueType { get; set; }
        public string state { get; set; }
        public object? value { get; set; } 
    }
}
