using DAL.Entities;
using NLog;

namespace Business.FactoryMethod
{
    public class ConcreteMessageSMS : IMessage
    {
        private readonly SentMessage _sm;

        public ConcreteMessageSMS(SentMessage sm)
        {
            _sm = sm;
        }
        public void ConcreteMessage(SchoolStaff ss, string sPhone)
        {
            //------------------------------------------------------------------------------------
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info("Sending SMS to employee " + ss.Name + " " + ss.Patronymic + " " + ss.Patronymic + ", address: " + sPhone + ", Text: " + _sm.MessageText);
        }
    }
}