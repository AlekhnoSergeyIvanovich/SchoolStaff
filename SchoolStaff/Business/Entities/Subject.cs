using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc; //??????????

namespace DAL.Entities
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Remote(action: "CheckName", controller: "Subject", ErrorMessage = "Name is already in use.")]
        public string Name { get; set; }
        public ICollection<SchoolStaffSubject> SchoolStaffSubjects { get; set; }

        public Subject()
        {
            SchoolStaffSubjects = new List<SchoolStaffSubject>();
        }
    }
}