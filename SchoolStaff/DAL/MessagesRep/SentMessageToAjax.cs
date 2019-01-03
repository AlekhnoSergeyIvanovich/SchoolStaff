using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.ChainOfResponsibility;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NLog;

namespace Business.MessagesRep
{
    public class SentMessageToAjax: ISentMessageToAjax
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public SentMessageToAjax(
            ApplicationDbContext context
            )
        {
            _context = context;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public object WriteMessage(SentMessage oSentMessage, int idSchoolStaff)
        {
            try
            {
                // TODO: Add insert logic here
                //factory method

                //----------------------------------------------
                SchoolStaff schoolStaff = _context.SchoolStaffs.Find(new object[] { idSchoolStaff });
                //SentMessage sentMessage = _context.SentMessages.Find(new object[] { idSentMessage });
                var ssp = _context.SchoolStaffPhones.First(c => c.SchoolStaffId == idSchoolStaff).Phone;

                try
                {
                    if ((ssp == String.Empty) && (schoolStaff.typeMessage == TypeMessage.Phone))
                    {
                        var error = new { success = false, message = "Warning" };
                        return error;
                    }

                    Receiver receiver = new Receiver(true, true);

                    SentMessageHandler smh = new SentMessageHandler();
                    DBSaveMessageHandler dbSavemh = new DBSaveMessageHandler();
                    smh.Successor = dbSavemh;
                    smh.Handle(receiver, oSentMessage, schoolStaff, ssp, _context);

                    var storiesObj = new { success = true, message = "Ok!!!" };
                    return storiesObj;
                }
                catch (Exception e)
                {
                    var error = new { success = false, message = "Error" };
                    _logger.Error(e, "Errow sent massege!!!");
                    return error;
                }
            }
            catch (Exception e)
            {
                var error = new { success = false };
                _logger.Error(e, "Errow work with message!!!");
                return error;
            }
        }

        public object WriteMessage([FromBody] JObject data)
        {
            var objSentMessage = data["idSentMessage"];
            int idSentMessage = 0;

            if (null != objSentMessage)
            {
                idSentMessage = int.Parse(objSentMessage.ToString());
            }

            var objSchoolStaff = data["idSchoolStaff"];
            int idSchoolStaff = 0;

            if (null != objSentMessage)
            {
                idSchoolStaff = int.Parse(objSchoolStaff.ToString());
            }

            SentMessage oSentMessage = _context.SentMessages.Find(new object[] { idSentMessage });
            SentMessage addMessage = new SentMessage()
            {
                IdSchoolStaff = oSentMessage.IdSchoolStaff,
                MessageText = oSentMessage.MessageText,
                Status = false,
                TypeMessage = oSentMessage.TypeMessage
            };

            return WriteMessage(addMessage, idSchoolStaff);
        }
    }
}
