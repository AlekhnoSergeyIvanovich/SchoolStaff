using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace Business.MainPhone
{
    public class CheckedPhone: ICheckedPhone
    {
        private readonly ApplicationDbContext _context;

        public CheckedPhone(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        public bool WritePhone(int idSchoolStaff, int? idPhone)
        {
            var ss = _context.SchoolStaffs.Find(idSchoolStaff);
            if (ss.PrimaryPhoneId != idPhone)
            {
                ss.PrimaryPhoneId = idPhone;
                _context.SchoolStaffs.Update(ss);
            }
            return true;
        }
    }
}
