﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadYourHeartOut.Models;
using Microsoft.Extensions.Logging;

namespace ReadYourHeartOut.Controllers
{
    public class HomeController : Controller
    {
        //ILogger here... dependency injection logger
        private readonly ILogger logger;
        public string Message;
  
        // constructor injection
        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            //Count++; 
            //ViewData["Count"] = Count + 1;
            ViewData["Message"] = $"Welcome to our awesome Site, you visited the site at: {DateTime.Now.ToLongTimeString()}";
            Message = $"Welcome to our awesome Site, you visited the site at: {DateTime.Now.ToLongTimeString()}";
            logger.LogDebug("Message displayed: {Message}", Message);
            //logger.LogError("Error displayed: {ErrorMessage}", ErrorMessage);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
