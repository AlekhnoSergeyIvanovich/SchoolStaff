using Business;
using Business.Repositories;
using DAL;
using DAL.Entities;

namespace Presentation.SignalR
{
    public class ProfessionDecorator : ServiceDecorator<Profession>, IRepository<Profession>
    {
        public ProfessionDecorator(
            Repository<Profession> _context,
            ApplicationDbContext context,
            IAppHub hub) : base(_context, context, hub)
        {
            base.MethodUpdate = "UpdateMessageProfession";
            base.MethodAdd = "AddMessageProfession";
            base.MethodRemove = "RemoveMessageProfession";
        }
    }
}