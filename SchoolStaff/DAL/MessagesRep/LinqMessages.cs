using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Entities;

namespace Business.MessagesRep
{
    public class LinqMessages: ILinqMessages
    {
        private readonly ApplicationDbContext _context;

        public LinqMessages(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        public IEnumerable<Messages> RetMessage()
        {
            var ret = from sentMessage in _context.SentMessages
                orderby sentMessage.Status, sentMessage.WriteTime descending
                      select new Messages()
                {
                    IdSentMessage = sentMessage.Id,
                    MessageText = sentMessage.MessageText,
                    TypeMessage = sentMessage.TypeMessage,
                    WriteTime = sentMessage.WriteTime,
                    Name = _context.SchoolStaffs.First(id => id.Id == sentMessage.IdSchoolStaff).RetFullName(),
                    Status = sentMessage.Status,
                    IdSchoolStaff = sentMessage.IdSchoolStaff
                      };
            return ret.AsEnumerable();
        }
    }
}