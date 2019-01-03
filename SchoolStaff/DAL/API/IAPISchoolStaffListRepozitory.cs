using System;
using System.Collections.Generic;
using System.Text;

namespace Business.API
{
    public interface IAPISchoolStaffListRepozitory
    {
        List<ApiSchoolStaff> ReturnAPISchoolStaffList();
    }
}
