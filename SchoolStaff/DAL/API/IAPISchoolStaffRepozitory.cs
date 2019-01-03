using System;
using System.Collections.Generic;
using System.Text;

namespace Business.API
{
    public interface IAPISchoolStaffRepozitory
    {
        ApiSchoolStaff ReturnAPISchoolStaffArray(int id);
    }
}
