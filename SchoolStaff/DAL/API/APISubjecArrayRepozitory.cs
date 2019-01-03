using System.Linq;
using DAL;

namespace Business.API
{
    public class APISubjecArrayRepozitory: IAPISubjecArrayRepozitory
    {
        private readonly ApplicationDbContext _context;

        public APISubjecArrayRepozitory(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        public ApiSubject[] ReturnAPISubjecArray(string subjecName)
        {
            var api = (from subj in _context.Subjects.Where(cc => ((subjecName == "") || (cc.Name == subjecName)))
                    orderby subj.Name
                    select new ApiSubject()
                    {
                        Id = subj.Id,
                        Name = subj.Name,
                        ApiSchoolStaffForSubject = ((from schStaffSubj in _context.SchoolStaffSubjects.Where(g => g.SubjectId == subj.Id)
                                join schStaff in _context.SchoolStaffs on schStaffSubj.SchoolStaffId equals schStaff.Id // into progSsp
                                select new ApiSchoolStaffForSubject()
                                {
                                    Id = schStaff.Id,
                                    Name = schStaff.Name,
                                    Surname = schStaff.Surname,
                                    Email = schStaff.Email,
                                    Patronymic = schStaff.Patronymic,
                                    Phones = (from schStaffPhone in _context.SchoolStaffPhones
                                            .Where(h => h.SchoolStaffId == schStaff.Id)
                                        select schStaffPhone.Phone).ToArray()
                                }
                            ).ToArray())
                    }
                ).ToArray();
            return api;
        }
    }
}