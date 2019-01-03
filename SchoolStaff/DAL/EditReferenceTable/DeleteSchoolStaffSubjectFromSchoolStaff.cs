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
    public class DeleteSchoolStaffSubjectFromSchoolStaff : IDeleteSchoolStaffSubjectFromSchoolStaff
    {
        private readonly ApplicationDbContext _context;

        public DeleteSchoolStaffSubjectFromSchoolStaff(
                ApplicationDbContext context
        )
        {
            _context = context;
        }

        public async Task procDeleteSchoolStaffSubjectFromSchoolStaff(Subject subject, int[] selectedSchoolStaffs)
        {
            var schoolStaffSubjectDelete = from t in _context.SchoolStaffSubjects.Where(es => es.SubjectId == subject.Id)
                where !selectedSchoolStaffs.Any(es => (es == t.SchoolStaffId))
                select t;

            foreach (var index in schoolStaffSubjectDelete)
            {
                _context.SchoolStaffSubjects.Remove(index);
            }

            var schoolStaffSubjectAdd = from s in selectedSchoolStaffs
                where !_context.SchoolStaffSubjects.Any(es => ((es.SchoolStaffId == s) && (es.SubjectId == subject.Id)))
                select new SchoolStaffSubject() { SchoolStaffId = s, SubjectId = subject.Id };

            foreach (var index in schoolStaffSubjectAdd)
            {
                await _context.SchoolStaffSubjects.AddAsync(index);
            }
        }
    }
}