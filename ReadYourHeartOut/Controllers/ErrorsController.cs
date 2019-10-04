using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ReadYourHeartOut.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("Error/{StatusCode}")]
        public IActionResult ErrorHandler(int StatusCode)
        {
            switch (StatusCode)
            {
                case 404:
                    ViewData["Message"] = "404 Not Found";
                    break;
            }
            return View("Error");
        }
    }
}