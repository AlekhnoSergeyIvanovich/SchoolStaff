using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DAL.Entities
{
    public class SchoolStaffProfession
    {
        [Key]
        public int Id { get; set; }

        [Remote(action: "VerifySchoolStaffProfession", controller: "Administration", AdditionalFields = "ProfessionId")]
        public int? SchoolStaffId { get; set; }
        public SchoolStaff SchoolStaff { get; set; }

        [Remote(action: "VerifySchoolStaffProfession", controller: "Administration", AdditionalFields = "SchoolStaffId")]
        public int? ProfessionId { get; set; }
        public Profession Profession { get; set; }
    }
}