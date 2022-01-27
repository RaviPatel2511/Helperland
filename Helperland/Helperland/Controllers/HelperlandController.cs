using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{

    public class HelperlandController : Controller
    {
        
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }
        public IActionResult Faqs()
        {
            ViewBag.Title = "faqs";
            return View();
        }
        public IActionResult Prices()
        {
            ViewBag.Title = "Prices";
            return View();
        }
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact us";
            return View();
        }
        public IActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }
        public IActionResult BecomeProvider()
        {
            ViewBag.Title = "BecomeProvider";
            return View();
        }
    }
}
