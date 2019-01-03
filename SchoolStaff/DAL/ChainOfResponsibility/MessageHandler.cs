using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;

namespace Business.ChainOfResponsibility
{
    public abstract class MessageHandler
    {
        public MessageHandler Successor { get; set; }
        public abstract void Handle(Receiver receiver, SentMessage smMessage, SchoolStaff ss, string ssp, ApplicationDbContext context);
    }
}
