namespace Business.MessagesRep.Validation
{
    public interface IVerifyTextMessage
    {
        VerifyTextMessageResalt VerifyMessage(string messageText, int IdSchoolStaff);
    }
}
