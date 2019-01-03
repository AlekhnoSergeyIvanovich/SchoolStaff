using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class SchoolStaffSubject
    {
        [Key]
        public int Id { get; set; }

        public int? SchoolStaffId { get; set; }
        public SchoolStaff SchoolStaff { get; set; }

        public int? SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
