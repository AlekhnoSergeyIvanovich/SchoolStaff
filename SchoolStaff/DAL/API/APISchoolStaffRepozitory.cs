using System;
using System.Collections;
using System.Linq;
using DAL;

namespace Business.API
{
    public class APISchoolStaffRepozitory : IAPISchoolStaffRepozitory
    {
        private readonly ApplicationDbContext _context;

        public APISchoolStaffRepozitory(
                ApplicationDbContext context
            )
        {
            _context = context;
        }

        public ApiSchoolStaff ReturnAPISchoolStaffArray(int id)
        {
            var api = from schStaff in _context.SchoolStaffs.Where(c => c.Id == id)
                select new ApiSchoolStaff()
                {
                    Id = schStaff.Id,
                    Surname = schStaff.Surname,
                    Name = schStaff.Name,
                    Patronymic = schStaff.Patronymic,
                    Email = schStaff.Email,
                    DateOfBirth = schStaff.DateOfBirth,
                    Phones = (from schStaffPhon in _context.SchoolStaffPhones.Where(k => k.SchoolStaffId == schStaff.Id)
                        select schStaffPhon.Phone).ToArray(),
                    Professions = ((from schStaffProf in _context.SchoolStaffProfessions.Where(g => g.SchoolStaffId == schStaff.Id)
                            join schStaffProfProf in _context.Professions on schStaffProf.ProfessionId equals schStaffProfProf.Id into progSsp
                            select progSsp.Select(c => c.NameProfession).First() //progSsp.ToList().Select(c => c.NameProfession)
                        ).ToArray()),
                    Subjects = ((from schStaffSubj in _context.SchoolStaffSubjects.Where(g => g.SchoolStaffId == schStaff.Id)
                            join schStaffSubjSubj in _context.Subjects on schStaffSubj.SchoolStaffId equals schStaffSubjSubj.Id into progSsp
                            select progSsp.Select(c => c.Name).First() //progSsp.ToList().Select(c => c.NameProfession)
                        ).ToArray())
                };

            return api.FirstOrDefault();
        }
    }

    //--------------------------------------------------------------------------------------------
    public class ApiSchoolStaff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public string[] Phones { get; set; }
        public string[] Subjects { get; set; }
        public string[] Professions { get; set; }

        public ApiSchoolStaff()
        {
            Phones = new string[] { };
            Subjects = new string[] { };
            Professions = new string[] { };
        }
    }
    //--------------------------------------------------------------------------------------------
    public class ApiSubject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ApiSchoolStaffForSubject[] ApiSchoolStaffForSubject { get; set; }

        public ApiSubject()
        {
            ApiSchoolStaffForSubject = new ApiSchoolStaffForSubject[] { };
        }
    }

    public class ApiSchoolStaffForSubject:IEnumerable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string[] Phones { get; set; }

        public ApiSchoolStaffForSubject()
        {
            Phones = new string[] { };
        }

        public IEnumerator GetEnumerator()
        {
            return Phones.GetEnumerator();
        }
    }

    public class ApiProfession
    {
        public int Id { get; set; }
        public string NameProfession { get; set; }
        public uint? CountPeopleProfession { get; set; }

        //[MaxLength(1)]
        public ApiSchoolStaffForProfession[] SchoolStaffProfession { get; set; }

        public ApiProfession()
        {
            SchoolStaffProfession = new ApiSchoolStaffForProfession[]{};
        }
    }

    public class ApiSchoolStaffForProfession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public string[] SchoolStaffPhones { get; set; }

        public string[] Subjects { get; set; }

        public ApiSchoolStaffForProfession()
        {
            SchoolStaffPhones = new string[]{};
            Subjects = new string[] { };
        }
    }
    //--------------------------------------------------------------------------------------------
}