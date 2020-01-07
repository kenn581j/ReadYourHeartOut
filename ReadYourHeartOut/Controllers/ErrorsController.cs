using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ReadYourHeartOut.Controllers
{
    public class ErrorsController : Controller
    {
        private readonly ILogger Logger;

        // Constructor injection
        public ErrorsController(ILogger<ErrorsController> logger)
        {
            this.Logger = logger;
        }

        // attribute routing
        [Route("Error/{StatusCode}")]
        public IActionResult ErrorHandler(int StatusCode)
        {//bruger slet ikke vores viewmodel, som vi har lavet
            switch (StatusCode)
            {
                case 404:
                    ViewData["Message"] = "404 Not Found";
                    break;
            }
            return View("Error");
        }
        // Atribute Routing: Denne metoden er mapped til at den skal køre hvis man kommer på routen ....../Error
        //man bliver automatisk ledet det sted hin: gik i Startup.cs i configure metoden
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult LogException()
        {
            IExceptionHandlerFeature exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewData["Message"] = exceptionDetails.Error.Message;
            Logger.LogCritical(exceptionDetails.Error.Message);

            return View("Error");
        }
    }
}