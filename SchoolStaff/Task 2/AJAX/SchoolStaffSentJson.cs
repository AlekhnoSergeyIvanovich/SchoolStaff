using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;

namespace Presentation.AJAX
{
    public struct SchoolStaffSentJson
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Surname { get; set; }

        public string DateOfBirth { get; set; }

        public string Email { get; set; }

        //public string Phone { get; set; }

        public bool IsExist { get; set; }

        public ICollection<SchoolStaffPhone> SchoolStaffPhones { get; set; }
    }
}