using System;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.ChainOfResponsibility
{
    public class DBSaveMessageHandler : MessageHandler
    {
        public override void Handle(Receiver receiver, SentMessage smMessage, SchoolStaff ss, string ssp, ApplicationDbContext context)
        {
            try
            {
                //smMessage.Id = 0;
                smMessage.Status = true;
                //context.SentMessages.Add(smMessage);
                //context.Entry(addMessage).State = EntityState.Added;
                context.SentMessages.Add(smMessage);

            }
            catch
            {
                try
                {
                    smMessage.Id = 0;
                    smMessage.Status = false;
                    //context.SentMessages.Add(smMessage);
                    context.SentMessages.Add(smMessage);
                }
                catch
                {
                    throw new Exception("Errow save to DB!!!");
                }
            }
        }
    }
}