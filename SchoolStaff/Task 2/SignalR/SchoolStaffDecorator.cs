using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Business.Repositories;
using DAL;
using DAL.Entities;

namespace Presentation.SignalR
{
    public class SchoolStaffDecorator: ServiceDecorator<SchoolStaff>, IRepository<SchoolStaff>
    {
        public SchoolStaffDecorator(
            Repository<SchoolStaff> _context,
            ApplicationDbContext context,
            IAppHub hub) : base(_context, context, hub)
        {
            base.MethodUpdate = "UpdateMessage";
            base.MethodAdd = "AddMessage";
            base.MethodRemove = "RemoveMessage";
        }
    }
}
