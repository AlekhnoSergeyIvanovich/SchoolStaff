using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class SchoolStaffPhone
    {
        [Key]
        public int Id { get; set; }

        [StringLength(15)]
        [RegularExpression(@"^\+[2-9]\d{2}-\d{2}-\d{7}$", ErrorMessage = "The phone number must be in the format: +xxx-xx-xxxxxxx")]
        public string Phone { get; set; }

        public int? SchoolStaffId { get; set; }
        public SchoolStaff SchoolStaff { get; set; }

        public SchoolStaff SchoolStaffPhoneOne { get; set; }
    }
}