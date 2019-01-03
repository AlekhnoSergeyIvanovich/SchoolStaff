using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Business.MessagesRep
{
    public interface ISentMessageToAjax
    {
        object WriteMessage(SentMessage oSentMessage, int idSchoolStaff);
        object WriteMessage([FromBody] JObject data);

    }
}
