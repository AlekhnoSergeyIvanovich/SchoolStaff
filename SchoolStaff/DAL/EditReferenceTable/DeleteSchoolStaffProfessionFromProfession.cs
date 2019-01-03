using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.EditReferenceTable;
using Business.Repositories;
using DAL;
using DAL.Entities;

namespace Business.Class
{
    public class DeleteSchoolStaffProfessionFromProfession : IDeleteSchoolStaffProfessionFromProfession
    {
        private readonly ApplicationDbContext _context;

        public DeleteSchoolStaffProfessionFromProfession(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        public async Task procDeleteSchoolStaffProfessionFromProfession(SchoolStaff schoolStaff, int[] selectedProfessions)
        {
            var a1 = _context.SchoolStaffProfessions.Where(es => es.SchoolStaffId == schoolStaff.Id);
            var schoolStaffProfessionDelete = from t in a1
                where !selectedProfessions.Any(es => (es == t.ProfessionId))
                select t;

            foreach (var index in schoolStaffProfessionDelete)
            {
                _context.SchoolStaffProfessions.Remove(index);
            }

            var schoolStaffProfessionAdd = from s in selectedProfessions
                where !_context.SchoolStaffProfessions.Any(es => ((es.ProfessionId == s) && (es.SchoolStaffId == schoolStaff.Id)))
                select new SchoolStaffProfession() { SchoolStaffId = schoolStaff.Id, ProfessionId = s };

            foreach (var index in schoolStaffProfessionAdd)
            {
                await _context.SchoolStaffProfessions.AddAsync(index);
            }
        }
    }
}
