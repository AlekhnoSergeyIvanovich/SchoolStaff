using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Business.EditReferenceTable
{
    public interface IDeleteSchoolStaffSubjectFromSchoolStaff
    {
        Task procDeleteSchoolStaffSubjectFromSchoolStaff(Subject subject, int[] selectedSchoolStaffs);
    }
}
