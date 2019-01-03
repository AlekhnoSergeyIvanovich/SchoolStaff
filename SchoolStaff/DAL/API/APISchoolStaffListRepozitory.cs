using System.Collections.Generic;
using System.Linq;
using DAL;

namespace Business.API
{
    public class APISchoolStaffListRepozitory: IAPISchoolStaffListRepozitory
    {
        private readonly ApplicationDbContext _context;

        public APISchoolStaffListRepozitory(
            ApplicationDbContext context
            )
        {
            _context = context;
        }

        public List<ApiSchoolStaff> ReturnAPISchoolStaffList()
        {
            var api = (from schStaf in _context.SchoolStaffs
                orderby schStaf.Surname, schStaf.Name, schStaf.Patronymic, schStaf.Email, schStaf.DateOfBirth descending
                select new ApiSchoolStaff()
                {
                    Id = schStaf.Id,
                    Surname = schStaf.Surname,
                    Name = schStaf.Name,
                    Patronymic = schStaf.Patronymic,
                    Email = schStaf.Email,
                    DateOfBirth = schStaf.DateOfBirth,
                    Phones = (from schStafPhon in _context.SchoolStaffPhones.Where(k => k.SchoolStaffId == schStaf.Id)
                        select schStafPhon.Phone).ToArray(),
                    Professions = ((from schStafProf in _context.SchoolStaffProfessions.Where(g => g.SchoolStaffId == schStaf.Id)
                            join prof in _context.Professions on schStafProf.ProfessionId equals prof.Id into progSsp
                            select progSsp.Select(c => c.NameProfession).First()
                        ).ToArray()),
                    Subjects = ((from schStafSubj in _context.SchoolStaffSubjects.Where(g => g.SchoolStaffId == schStaf.Id)
                            join subj in _context.Subjects on schStafSubj.SubjectId equals subj.Id into progSsp
                            select progSsp.Select(c => c.Name).First()
                        ).ToArray())
                        
                }).ToList();
            return api;
        }
    }
}