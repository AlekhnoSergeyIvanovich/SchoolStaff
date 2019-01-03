using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DAL.Entities
{
    public class Profession
    {
        [Key]
        public int Id { get; set; }
        [Remote(action: "VerifyName", controller: "Administration", ErrorMessage = "Name is already in use.", AdditionalFields = "Id")]
        public string NameProfession { get; set; }
        [Remote(action: "VerifyCountPeopleProfession", controller: "Administration", ErrorMessage = "Name is already in use.", AdditionalFields = "Id")]
        public uint? CountPeopleProfession { get; set; }

        //[MaxLength(1)]
        public ICollection<SchoolStaffProfession> SchoolStaffProfession { get; set; }

        public Profession()
        {
            SchoolStaffProfession = new List<SchoolStaffProfession>();
        }
    }
}