using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntelligentClassroom.Models.DTO
{
    public class Resource
    {
        #region [-ctor-]
        public Resource()
        {

        } 
        #endregion

        public List<Models.DTO.Sensor> Sensors { get; set; }
        public List<Models.DTO.Actuator> Actuators { get; set; }

        public int Ref_Device { get; set; }

    }
}