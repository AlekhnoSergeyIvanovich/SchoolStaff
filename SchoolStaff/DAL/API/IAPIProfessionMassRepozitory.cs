using System;
using System.Collections.Generic;
using System.Text;

namespace Business.API
{
    public interface IAPIProfessionArrayRepozitory
    {
        ApiProfession[] ReturnAPIProfessionArray(string professionName);
    }
}
