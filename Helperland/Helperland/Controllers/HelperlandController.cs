using Helperland.Models;
using Helperland.Models.Data;
using Helperland.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class HelperlandController : Controller
    {
        private readonly HelperlandContext _helperlandContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HelperlandController(HelperlandContext helperlandContext, IWebHostEnvironment webHostEnvironment )
        {
            _helperlandContext = helperlandContext;
            _webHostEnvironment = webHostEnvironment;
        }
        [Route("")]
        [Route("Home")]
        public IActionResult Index(bool isLoginerror=false)
        {
            ViewBag.IsLoginerror = isLoginerror;
            ViewBag.Title = "Home";
            return View();
        }
        [Route("faqs")]
        public IActionResult Faqs()
        {
            ViewBag.Title = "faqs";
            return View();
        }
        [Route("price")]
        public IActionResult Prices()
        {
            ViewBag.Title = "Prices";
            return View();
        }
        //[Route("contact")]
        public IActionResult Contact(bool isSuccess=false)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.Title = "Contact us";
            return View();
        }
        [HttpPost]
        public  IActionResult Contact(contactusform contactform)
        {
            if (ModelState.IsValid)
            {
                if(contactform.UploadFileName != null)
                {
                    string folder = "contactUsUploadedFile/";
                    folder += Guid.NewGuid().ToString() + "_" +contactform.UploadFileName.FileName;
                    contactform.UploadFilePath = folder;
                    string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    contactform.UploadFileName.CopyToAsync(new FileStream(serverfolder, FileMode.Create)); ;
                }
                var request = new ContactUs()
                {
                    Name = contactform.fname + " " + contactform.lname,
                    Email = contactform.Email,
                    Subject = contactform.Subject,
                    PhoneNumber = contactform.PhoneNumber,
                    Message = contactform.Message,
                    CreatedOn = DateTime.UtcNow,
                    UploadFileName = contactform.UploadFilePath,
                    FileName = contactform.UploadFileName.FileName,
            };
            _helperlandContext.ContactUs.Add(request);
            _helperlandContext.SaveChanges();
            return RedirectToAction(nameof(Contact), new {isSuccess = true});

            }
            return View();
        }
        [Route("about")]
        public IActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }
    }
}
