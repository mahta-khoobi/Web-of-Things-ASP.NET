using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntelligentClassroom.Models.POCO
{
    public class SensorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
        public string Value { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
        public Nullable<int> Frequency { get; set; }

        public Nullable<int> Device_Ref { get; set; }
    }
}