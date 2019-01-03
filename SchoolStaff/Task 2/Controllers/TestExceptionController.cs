using System;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class TestExceptionController : Controller
    {
        public void Index()
        {
            throw new Exception("Test");
        }
    }
}