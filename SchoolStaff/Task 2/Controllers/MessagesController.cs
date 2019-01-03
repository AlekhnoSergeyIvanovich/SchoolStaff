using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.MessagesRep;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ILinqMessages _LinqMessages;

        public MessagesController(
            ILinqMessages linqMessages
        )
        {
            _LinqMessages = linqMessages;
        }

        // GET: Messages
        public ActionResult Index()
        {
            return View(_LinqMessages.RetMessage());
        }
    }
}