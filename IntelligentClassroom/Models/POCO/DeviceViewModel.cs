using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntelligentClassroom.Models.POCO
{
    public class DeviceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CurrentStatus { get; set; }
        public string Version { get; set; }

        public Models.POCO.SensorViewModel Sensor { get; set; }
        public Models.EF.Actuator Actuator { get; set; }
    }

}