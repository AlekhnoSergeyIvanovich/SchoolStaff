using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Business.EditReferenceTable
{
    public interface IDeleteSchoolStaffSubjectFromSubject
    {
        Task procDeleteSchoolStaffSubjectFromSubject(SchoolStaff schoolStaff, int[] selectedSubject);
    }
}
