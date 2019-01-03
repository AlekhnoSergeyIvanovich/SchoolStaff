using System;
using System.Collections.Generic;
using System.Text;

namespace Business.MessagesRep.Validation
{
    public enum VerifyTextMessageResalt
    {
        MailLengthLonger500Characters,
        MailTrue,
        SmsLengthLonger120Characters,
        PhonesNotEntered,
        PhonesTrue,
        TypeNotDefined
    }
}
