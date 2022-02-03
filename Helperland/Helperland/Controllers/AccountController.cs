using Helperland.Models;
using Helperland.Models.Data;
using Helperland.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class AccountController : Controller
    {
    private readonly HelperlandContext _helperlandContext;
        public AccountController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }

      
        string subject = "Password reseat link.";
        string mailTitle = "Helperland Service";
        string fromEmail = "ravipatelphoto@gmail.com";
        string fromEmailPassword = "Ravi@2511";
        public IActionResult CustSignup(bool isuserExists= false)
        {
            if(HttpContext.Session.GetString("userid") == null)
            {
                    ViewBag.Title = "Signup";
                    ViewBag.IsuserExists = isuserExists;
                    return View();
            }
            return RedirectToAction("Error", "Helperland");
        }
        [HttpPost]
        public IActionResult CustSignup(User user)
        {
            
            if (ModelState.IsValid)
            {

                user.UserTypeId = 1;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                var userExists = _helperlandContext.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if (userExists == null)
                {
                    _helperlandContext.Users.Add(user);
                    _helperlandContext.SaveChanges();
                    return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
                }
                else
                {
                    return RedirectToAction(nameof(CustSignup), new { isuserExists = true });
                }
            }
           
                return View();
            
        }
        public IActionResult ProviderSignup(bool isproviderExists = false)
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                ViewBag.Title = "Signup";
                ViewBag.IsproviderExists = isproviderExists;
                return View();
            }
            return RedirectToAction("Error", "Helperland");
        }
        [HttpPost]
        public IActionResult ProviderSignup(User user)
        {
            if (ModelState.IsValid)
            {

                user.UserTypeId = 2;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;

           
                var userExists = _helperlandContext.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if (userExists == null)
                {
                    _helperlandContext.Users.Add(user);
                    _helperlandContext.SaveChanges();
                    return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
                }
                else
                {
                    return RedirectToAction(nameof(ProviderSignup), new { isproviderExists = true });
                }
            }
            
                return View();
           
        }
        [HttpPost]
        public IActionResult login(User user)
        {

            if(user.Email!=null && user.Password != null)
            {
                var credentials = _helperlandContext.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
                if (credentials != null)
                {
                    HttpContext.Session.SetString("userid", credentials.UserId.ToString());
                    HttpContext.Session.SetString("usertypeid", credentials.UserTypeId.ToString());
                    if (credentials.UserTypeId == 1)
                    {
                        return RedirectToAction("ServiceHistory", "Customer");
                    }   
                    else if (credentials.UserTypeId == 2)
                    {
                        return RedirectToAction("UpcomingService", "Provider");
                    }
                }
  
            }
            TempData["notfound"] = "NotFound";
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpPost]

        public IActionResult ForgotPassword(User user)
        {
            if(user.Email != null)
            {
                User account = _helperlandContext.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if (account != null)
                {
                    var unique = account.Password;
                    string resetcode = BCrypt.Net.BCrypt.HashPassword(unique);
                    string MailBody = "<!DOCTYPE html>" +
                             "<html> " +
                                 "<body style=\"background -color:#ff7f26;text-align:center;\"> " +
                                 "<h1 style=\"color:#051a80;\">Welcome to Helperland</h1> " +
                                 "<h2 style=\"color:#fff;\">Tap on reset password below link to reset your password</h2> " +
                                  "<a href='" + Url.Action("ResetPass", "Account", new { code = resetcode, id = account.UserId }, "http") + "'>Reset Password</a>" +
                                 "</body> " +
                             "</html>";
                    MailMessage message = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress(account.Email));
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
                    return RedirectToAction("About", "Helperland");
                }
            }
            return RedirectToAction("Contact", "Helperland");
        }
        [HttpGet]
        public IActionResult ResetPass(string code, int id)
        {
            User user = _helperlandContext.Users.Where(x => x.UserId == id).FirstOrDefault();
            string uniquefield = user.Password;
            bool isValidlink = BCrypt.Net.BCrypt.Verify(uniquefield, code);
            if (isValidlink)
            {
                return View();
            }
            return RedirectToAction("Error", "Helperland");
        }
        [HttpPost]
        public IActionResult ResetPass(int id,ResetPass resetpass)
        {
            if (ModelState.IsValid)
            {
            User user = _helperlandContext.Users.Where(x => x.UserId == id).FirstOrDefault();
            user.Password = resetpass.password;
            _helperlandContext.Users.Update(user);
            _helperlandContext.SaveChanges();
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));

            }
            else
            {
                return View();
            }
        }

    }
}
