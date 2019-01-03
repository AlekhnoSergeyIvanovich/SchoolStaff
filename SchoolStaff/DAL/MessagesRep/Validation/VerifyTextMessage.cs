using System.Linq;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Business.MessagesRep.Validation
{
    public class VerifyTextMessage: IVerifyTextMessage
    {

        private readonly ApplicationDbContext _context;

        public VerifyTextMessage(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        public VerifyTextMessageResalt VerifyMessage(string messageText, int idSchoolStaff)
        {
            var typeMessage = _context.SchoolStaffs.Find(idSchoolStaff).typeMessage;
            switch (typeMessage)
            {
                case TypeMessage.Email:
                    if (messageText.Length > 500)
                    {
                        return VerifyTextMessageResalt.MailLengthLonger500Characters;// Json(data: $"Mail length is no longer than 500 characters.");
                    }
                    else
                    {
                        return VerifyTextMessageResalt.MailTrue;// Json(data: true);
                    }

                case TypeMessage.Phone:
                    if (messageText.Length > 120)
                    {
                        return VerifyTextMessageResalt
                            .SmsLengthLonger120Characters; //Json(data: $"SMS length is no longer than 120 characters.");
                    }
                    else
                    {
                        var ss = _context.SchoolStaffPhones.Count(c => c.SchoolStaffId == idSchoolStaff);
                        if (ss == 0)
                        {
                            return
                                VerifyTextMessageResalt
                                    .PhonesNotEntered; //Json(data: $"Phones not entered."); //"Destination address is not provided"
                        }
                        else
                            return VerifyTextMessageResalt.PhonesTrue; //Json(data: true);
                    }
                default: return VerifyTextMessageResalt.TypeNotDefined; //Json(data: "Type not defined");
            }
        }
    }
}