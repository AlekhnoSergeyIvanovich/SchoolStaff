using System.Linq;
using DAL;

namespace Business.API
{
    public class APIProfessionArrayRepozitory : IAPIProfessionArrayRepozitory
    {
        private readonly ApplicationDbContext _context;

        public APIProfessionArrayRepozitory(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        public ApiProfession[] ReturnAPIProfessionArray(string  professionName)
        {
            var api = (from prof in _context.Professions.Where(cc => ((professionName == "") || (cc.NameProfession == professionName)))
                       orderby prof.NameProfession
                       select new ApiProfession()
                       {
                           Id = prof.Id,
                           NameProfession = prof.NameProfession,
                           CountPeopleProfession = prof.CountPeopleProfession - (uint)(from schstafProf in _context.SchoolStaffProfessions.Where(df => df.ProfessionId == prof.Id)
                                                                                    select schstafProf.SchoolStaffId).Count(),
                           SchoolStaffProfession = ((from schstafProf in _context.SchoolStaffProfessions.Where(g => g.ProfessionId == prof.Id)
                                                     join schstaf in _context.SchoolStaffs on schstafProf.SchoolStaffId equals schstaf.Id
                                                     select new ApiSchoolStaffForProfession()
                                                     {
                                                         Id = schstaf.Id,
                                                         Name = schstaf.Name,
                                                         Surname = schstaf.Surname,
                                                         Email = schstaf.Email,
                                                         Patronymic = schstaf.Patronymic,
                                                         DateOfBirth = schstaf.DateOfBirth,
                                                         SchoolStaffPhones = (from schstafPhon in _context.SchoolStaffPhones
                                                                 .Where(h => h.SchoolStaffId == schstaf.Id)
                                                                              select schstafPhon.Phone).ToArray(),
                                                         Subjects = (from schstafSubje in _context.SchoolStaffSubjects.Where(jh => jh.SchoolStaffId == schstaf.Id)
                                                                     join subje in _context.Subjects on schstafSubje.SubjectId equals subje.Id
                                                                     select subje.Name
                                                                 ).ToArray()
                                                     }
                               ).ToArray())
                       }).ToArray();
            return api;
        }
    }
}