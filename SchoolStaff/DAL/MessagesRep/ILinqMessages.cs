﻿using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace Business.MessagesRep
{
    public interface ILinqMessages
    {
        IEnumerable<Messages> RetMessage();
    }
}
