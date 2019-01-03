using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace DAL.Entities
{
    public class SentMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdSchoolStaff { get; set; }

        [Required]
        [Remote(action: "VerifyMessage", controller: "SentMessage", AdditionalFields = "IdSchoolStaff")]
        [StringLength(500, MinimumLength = 3)]
        public string MessageText { get; set; }

        [Required]
        [Display(Name = "Send message type")]
        public TypeMessage TypeMessage { get; set; }

        //[Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime WriteTime { get; set; }

        [Required]
        public bool Status { get; set; }

        //[Required]
        public SchoolStaff SchoolStaff { get; set; }
    }
}