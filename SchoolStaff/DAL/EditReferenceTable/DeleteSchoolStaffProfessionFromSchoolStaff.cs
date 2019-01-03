using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Class;
using Business.Repositories;
using DAL;
using DAL.Entities;

namespace Business.EditReferenceTable
{
    public class DeleteSchoolStaffProfessionFromSchoolStaff : IDeleteSchoolStaffProfessionFromSchoolStaff
    {
        private readonly ApplicationDbContext _context;

        public DeleteSchoolStaffProfessionFromSchoolStaff(
            ApplicationDbContext context
            )
        {
            _context = context;
        }

        public async Task procDeleteSchoolStaffProfessionFromSchoolStaff(Profession profession, int[] selectedSchoolStaffs)
        {
            var schoolStaffProfessionDelete = from t in _context.SchoolStaffProfessions.Where(a => a.ProfessionId == profession.Id)
                where !selectedSchoolStaffs.Any(es => (es == t.SchoolStaffId))
                select t;

            foreach (var index in schoolStaffProfessionDelete)
            {
                _context.SchoolStaffProfessions.Remove(index);
            }

            var schoolStaffProfessionAdd = from s in selectedSchoolStaffs
                where !_context.SchoolStaffProfessions.Any(es => ((es.SchoolStaffId == s) && (es.ProfessionId == profession.Id)))
                select new SchoolStaffProfession() { SchoolStaffId = s, ProfessionId = profession.Id };

            foreach (var index in schoolStaffProfessionAdd)
            {
                await _context.SchoolStaffProfessions.AddAsync(index);
            }
        }
    }
}