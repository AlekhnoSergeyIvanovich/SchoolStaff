using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;

namespace DAL.Entities
{
    public class SchoolStaff
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Patronymic { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Surname { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Send message type")]
        public TypeMessage typeMessage { get; set; }

        public int? PrimaryPhoneId { get; set; }

        public SchoolStaffPhone SsPhone { get; set; }

        public ICollection<SchoolStaffPhone> SchoolStaffPhones { get; set; }

        public ICollection<SchoolStaffSubject> SchoolStaffSubjects { get; set; }

        public ICollection<SchoolStaffProfession> SchoolStaffProfession { get; set; }

        [Required]
        public ICollection<SentMessage> SentMessage { get; set; }

        public SchoolStaff()
        {
            SchoolStaffPhones = new List<SchoolStaffPhone>();
            SchoolStaffSubjects = new List<SchoolStaffSubject>();
            SchoolStaffProfession = new List<SchoolStaffProfession>();
            SentMessage = new List<SentMessage>();
        }

        public string RetFullName()
        {
            return Surname + " " + Name + " " + Patronymic;
        }

        //public IEnumerator GetEnumerator()
        //{
        //    return SchoolStaffPhones.GetEnumerator();
        //}
    }
}