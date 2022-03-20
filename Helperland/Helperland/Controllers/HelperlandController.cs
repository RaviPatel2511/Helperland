using Helperland.Models;
using Helperland.Models.Data;
using Helperland.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            if(logedUserid != null)
            {
                User logedUser = _helperlandContext.Users.Where(x => x.UserId == logedUserid).FirstOrDefault();
                ViewBag.IsloggedIn = "success";
                ViewBag.UserName = logedUser.FirstName;
                ViewBag.UType = logedUser.UserTypeId;
            }
            else
            {
                ViewBag.UType = 1;
            }
            ViewBag.IsLoginerror = isLoginerror;
            ViewBag.Title = "Home";
            return View();
        }
        [Route("faqs")]
        public IActionResult Faqs()
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            if (logedUserid != null)
            {
                User logedUser = _helperlandContext.Users.Where(x => x.UserId == logedUserid).FirstOrDefault();
                ViewBag.IsloggedIn = "success";
                ViewBag.UserName = logedUser.FirstName;
                ViewBag.UType = logedUser.UserTypeId;
            }
            else
            {
                ViewBag.UType = 1;
            }

            ViewBag.Title = "faqs";
            return View();
        }
        [Route("price")]
        public IActionResult Prices()
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            if (logedUserid != null)
            {
                User logedUser = _helperlandContext.Users.Where(x => x.UserId == logedUserid).FirstOrDefault();
                ViewBag.IsloggedIn = "success";
                ViewBag.UserName = logedUser.FirstName;
                ViewBag.UType = logedUser.UserTypeId;
            }
            else
            {
                ViewBag.UType = 1;
            }
            ViewBag.Title = "Prices";
            return View();
        }
        //[Route("contact")]
        public IActionResult Contact(bool isSuccess=false)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            if (logedUserid != null)
            {
                User logedUser = _helperlandContext.Users.Where(x => x.UserId == logedUserid).FirstOrDefault();
                ViewBag.IsloggedIn = "success";
                ViewBag.UserName = logedUser.FirstName;
                ViewBag.UType = logedUser.UserTypeId;
            }
            else
            {
                ViewBag.UType = 1;
            }
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

                string subject = "Customer Wants some help from you !";
                string mailTitle = "Helperland Service";
                string fromEmail = "codewithravi2511@gmail.com";
                string fromEmailPassword = "dyto qxph hvgv oslf";


                string MailBody = "<!DOCTYPE html>" +
                         "<html> " +
                             "<body style=\"background -color:#ff7f26;text-align:center;\"> " +
                             "<h1 style=\"color:#051a80;\">Welcome to Helperland.</h1> " +
                             "<p>Dear Admin,</p>" +
                              "<p>Customer wants some help from your side Here is the Info of request from customer, </p>" +
                              "<p>Name : "+ request.Name + "</p>" +
                              "<p>Email : " + request.Email + "</p>" +
                              "<p>Subject : " + request.Subject + "</p>" +
                              "<p>PhoneNumber : " + request.PhoneNumber + "</p>" +
                              "<p>Message : " + request.Message + "</p>" +
                              "<p>upload FileName : " + request.FileName + "</p>" +
                             "</body> " +
                         "</html>";
                MailMessage message = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress("admin001@yopmail.com"));
                message.Subject = subject;
                message.Body = MailBody;
                message.IsBodyHtml = true;

                //Server Details
                SmtpClient smtp = new SmtpClient();
                //Outlook ports - 465 (SSL) or 587 (TLS)
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                //Credentials
                System.Net.NetworkCredential credential = new System.Net.NetworkCredential();
                credential.UserName = fromEmail;
                credential.Password = fromEmailPassword;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credential;

                smtp.Send(message);

                return RedirectToAction(nameof(Contact), new {isSuccess = true});

            }
            return View();
        }
        [Route("about")]
        public IActionResult About()
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            if (logedUserid != null)
            {
                User logedUser = _helperlandContext.Users.Where(x => x.UserId == logedUserid).FirstOrDefault();
                ViewBag.IsloggedIn = "success";
                ViewBag.UserName = logedUser.FirstName;
                ViewBag.UType = logedUser.UserTypeId;
            }
            else
            {
                ViewBag.UType = 1;
            }
            ViewBag.Title = "About";
            return View();
        } 
        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
