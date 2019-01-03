using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Business.EditReferenceTable
{
    public interface IDeleteSchoolStaffProfessionFromSchoolStaff
    {
        Task procDeleteSchoolStaffProfessionFromSchoolStaff(Profession profession, int[] selectedSchoolStaffs);
    }
}
