using System;
using System.Linq;
using Business.ChainOfResponsibility;
using Business.FactoryMethod;
using Business.MessagesRep;
using Business.MessagesRep.Validation;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NLog;
using NLog.Web;

namespace Presentation.Controllers
{
    public class SentMessageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly ISentMessageToAjax _sentMessageToAjax;  //, SentMessageToAjax
        private readonly IVerifyTextMessage _iVerifyTextMessage;  // VerifyTextMessage

        public SentMessageController(
            ApplicationDbContext context,
            ISentMessageToAjax sentMessageToAjax,
            IVerifyTextMessage iVerifyTextMessage
            )
        {
            _context = context;
            _logger = LogManager.GetCurrentClassLogger();
            _sentMessageToAjax = sentMessageToAjax;
            _iVerifyTextMessage = iVerifyTextMessage;
        }

        // GET: SentMessage/Create
        public ActionResult Create(int idSchoolStaff)
        {
            SchoolStaff ss = _context.SchoolStaffs.Find(new object[] { idSchoolStaff});

            ViewBag.idSchoolStaff = idSchoolStaff;

            SentMessage sm = new SentMessage()
            {
                TypeMessage = ss.typeMessage,
                IdSchoolStaff = ss.Id,
                SchoolStaff = ss
            };

            return View(sm);
        }

        // POST: SentMessage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SentMessage smMessage, int idSchoolStaff)
        {
            _sentMessageToAjax.WriteMessage(smMessage, idSchoolStaff);
            return RedirectToAction("Index", "SchoolStaff");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyMessage(string messageText, int IdSchoolStaff)
        {
            switch (_iVerifyTextMessage.VerifyMessage(messageText, IdSchoolStaff))
            {
                case VerifyTextMessageResalt.MailLengthLonger500Characters:
                    return Json(data: $"Mail length is no longer than 500 characters.");
                case VerifyTextMessageResalt.MailTrue:
                    return Json(data: true);
                case VerifyTextMessageResalt.SmsLengthLonger120Characters:
                    return Json(data: $"SMS length is no longer than 120 characters.");
                case VerifyTextMessageResalt.PhonesNotEntered:
                    return Json(data: $"Phones not entered.");
                case VerifyTextMessageResalt.PhonesTrue:
                    return Json(data: true);
                case VerifyTextMessageResalt.TypeNotDefined:
                    return Json(data: "Type not defined");
                default:
                    return Json(data: "Type not defined");

            }
        }

        [HttpPost]
        [Route("SentMessage/WriteMessage")]
        public object WriteMessage([FromBody] JObject data/*, [FromBody]int idSchoolStaff*/)
        {
            return _sentMessageToAjax.WriteMessage(data);
        }
    }
}