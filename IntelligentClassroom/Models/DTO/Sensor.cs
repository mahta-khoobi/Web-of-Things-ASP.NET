using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntelligentClassroom.Models.DTO
{
    public class Sensor
    {
        #region [-ctor-]
        public Sensor()
        {

        }
        #endregion

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
        public string Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Frequency { get; set; }

    }
}