using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntelligentClassroom.Models.POCO
{
    public class ClassTime
    {
        public int ClassCode { get; set; }
        public string ClassName { get; set; }
        public int CourseCode { get; set; }
        public DayOfWeek CourseDay { get; set; }
        public DateTime CourseTime { get; set; }
    }
}