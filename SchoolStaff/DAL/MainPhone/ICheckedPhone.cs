using System;
using System.Collections.Generic;
using System.Text;

namespace Business.MainPhone
{
    public interface ICheckedPhone
    {
        bool WritePhone(int idSchoolStaff, int? idPhone);
    }
}
