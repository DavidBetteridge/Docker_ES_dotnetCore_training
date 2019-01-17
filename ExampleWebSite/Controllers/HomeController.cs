using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExampleWebSite.Models;
using System.IO;

namespace ExampleWebSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!Directory.Exists("/Data"))
            {
                ViewData["Message"] = "The /Data folder does not exist";
            }
            else if (!System.IO.File.Exists("/Data/data.txt"))
            {
                ViewData["Message"] = "The /Data/data.txt file does not exist";
            }
            else
            {
                ViewData["Message"] = System.IO.File.ReadAllText("/Data/data.txt");
            }

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
