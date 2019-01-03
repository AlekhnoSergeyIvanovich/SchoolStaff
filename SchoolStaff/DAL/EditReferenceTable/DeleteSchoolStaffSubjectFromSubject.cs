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
    public class DeleteSchoolStaffSubjectFromSubject: IDeleteSchoolStaffSubjectFromSubject
    {
        private readonly ApplicationDbContext _context;

        public DeleteSchoolStaffSubjectFromSubject(
                ApplicationDbContext context
            )
        {
            _context = context;
        }

        public async Task procDeleteSchoolStaffSubjectFromSubject(SchoolStaff schoolStaff, int[] selectedSubject)
        {
            var schoolStaffSubjectDelete = from t in _context.SchoolStaffSubjects.Where(es => es.SchoolStaffId == schoolStaff.Id)
                where !selectedSubject.Any(es => (es == t.SubjectId))
                select t;

            foreach (var index in schoolStaffSubjectDelete)
            {
                _context.SchoolStaffSubjects.Remove(index);
            }

            var schoolStaffSubjectAdd = from s in selectedSubject
                where !_context.SchoolStaffSubjects.Any(es => ((es.SubjectId == s) && (es.SchoolStaffId == schoolStaff.Id)))
                select new SchoolStaffSubject() { SchoolStaffId = schoolStaff.Id, SubjectId = s };

            foreach (var index in schoolStaffSubjectAdd)
            {
                await _context.SchoolStaffSubjects.AddAsync(index);
            }
        }
    }
}
