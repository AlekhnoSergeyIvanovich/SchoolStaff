using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Messages
    {
        public int IdSentMessage { get; set; }
        public string Name { get; set; }
        public string MessageText { get; set; }
        public TypeMessage TypeMessage { get; set; }
        public DateTime WriteTime { get; set; }
        public bool Status { get; set; }
        public int IdSchoolStaff { get; set; }
    }
}
