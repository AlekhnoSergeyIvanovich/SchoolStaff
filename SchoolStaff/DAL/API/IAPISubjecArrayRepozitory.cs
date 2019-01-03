using System;
using System.Collections.Generic;
using System.Text;

namespace Business.API
{
    public interface IAPISubjecArrayRepozitory
    {
        ApiSubject[] ReturnAPISubjecArray(string subjecName);
    }
}
