using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;

namespace Business.FactoryMethod
{
    public interface ICreator
    {
        IMessage FactoryMethod(SentMessage cm);
    }
}
