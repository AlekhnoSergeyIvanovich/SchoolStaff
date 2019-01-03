using DAL.Entities;

namespace Business.FactoryMethod
{
    public class ConcreteCreator: ICreator
    {
        public IMessage FactoryMethod(SentMessage sm)
        {
            switch (sm.TypeMessage)
            {
                case TypeMessage.Email:
                    return new ConcreteMessageEmail(sm);
                case TypeMessage.Phone:
                    return new ConcreteMessageSMS(sm);
                default:
                    return new ConcreteMessageEmail(sm);
            }
        }
    }
}