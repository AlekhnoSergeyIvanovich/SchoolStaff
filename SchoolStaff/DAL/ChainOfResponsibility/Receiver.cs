using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.ChainOfResponsibility
{
    public class Receiver
    {
        public bool SentMessage { get; set; }
        public bool WriteDb { get; set; }

        public Receiver(bool sm, bool wdb)
        {
            SentMessage = sm;
            WriteDb = wdb;
        }
    }
}
