using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;

namespace Business.FactoryMethod
{
    public interface IMessage
    {
        void ConcreteMessage(SchoolStaff ss, string sPhone);
    }
}