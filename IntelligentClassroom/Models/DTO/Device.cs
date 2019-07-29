using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntelligentClassroom.Models.DTO
{
    public class Device
    {
        #region [-ctor-]
        public Device()
        {

        }
        #endregion

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       // public string Url { get; set; }
        public string CurrentStatus { get; set; }
        public string Version { get; set; }
        public Models.DTO.Resource Resource { get; set; }

    }
}