using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Business.EditReferenceTable
{
    public interface IDeleteSchoolStaffProfessionFromProfession
    {
        Task procDeleteSchoolStaffProfessionFromProfession(SchoolStaff schoolStaff, int[] selectedProfessions);
    }
}
