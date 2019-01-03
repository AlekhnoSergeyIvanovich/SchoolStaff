using System;
using Business.FactoryMethod;
using DAL;
using DAL.Entities;

namespace Business.ChainOfResponsibility
{
    public class SentMessageHandler: MessageHandler
    {
        public override void Handle(Receiver receiver, SentMessage smMessage, SchoolStaff ss, string ssp,
            ApplicationDbContext context)
        {
            if (receiver.SentMessage == true)
            {
                try
                { 
                    if ((smMessage.TypeMessage == TypeMessage.Phone) && (ssp == string.Empty))
                    {
                        receiver.WriteDb = false;
                    }
                    else
                    {
                        var cc = new ConcreteCreator();
                        var a = cc.FactoryMethod(smMessage);
                        a.ConcreteMessage(ss, ssp);
                        receiver.WriteDb = true;
                        smMessage.Status = true;
                    }
                }
                catch (Exception e)
                {
                    smMessage.Status = false;
                    Console.WriteLine(e);
                    throw;
                }

                if (Successor != null)
                    Successor.Handle(receiver, smMessage, ss, ssp, context);
            }
        }
    }
}