using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DAL.Entities;
using NLog;

namespace Business.FactoryMethod
{
    public class ConcreteMessageEmail : IMessage
    {
        private SentMessage _sm;

        public ConcreteMessageEmail(SentMessage sm)
        {
            _sm = sm;
        }

        public void ConcreteMessage(SchoolStaff ss, string sPhone)
        {

                var fromAddress = new MailAddress("alekhnosergey86@gmail.com");
                var fromPassword = "win32dok";
                var toAddress = new MailAddress(ss.Email);

                string subject = "subject";
                string body = _sm.MessageText;

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })

                try
                {
                    smtp.Send(message);
                    var logger = LogManager.GetCurrentClassLogger();
                    logger.Info("Sending email to employee " + ss.Name + " " + ss.Patronymic + " " + ss.Patronymic + ", address: " + ss.Email + ", Text: " + _sm.MessageText);
                }
                catch (Exception ex)
                {
                    var logger = LogManager.GetCurrentClassLogger();
                    logger.Info("Exception caught in CreateTestMessage4(): {0}",
                        ex.ToString());
                }
                //------------------------------------------------------------------------------------
        }
    }
}