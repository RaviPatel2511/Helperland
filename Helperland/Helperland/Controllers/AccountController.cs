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
        string fromEmail = "codewithravi2511@gmail.com";
        string fromEmailPassword = "dyto qxph hvgv oslf";
        public IActionResult CustSignup(bool isuserExists= false)
        {
            if(HttpContext.Session.GetInt32("userid") == null)
            {
                    ViewBag.Title = "Signup";
                    ViewBag.IsuserExists = isuserExists;
                    ViewBag.UType = 1;
                    return View();
            }
            return RedirectToAction("Error", "Helperland");
        }
        [HttpPost]
        public IActionResult CustSignup(User user)
        {
            
            if (ModelState.IsValid)
            {

               
                var userExists = _helperlandContext.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if (userExists == null)
                {
                    user.UserTypeId = 1;
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    user.CreatedDate = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    user.IsActive = true;
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
        [HttpGet]
        public IActionResult ProviderSignup(bool isproviderExists = false)
        {
            if (HttpContext.Session.GetInt32("userid") == null)
            {
                ViewBag.Title = "Signup";
                ViewBag.IsproviderExists = isproviderExists;
                ViewBag.UType = 1;
                return View();
            }
            return RedirectToAction("Error", "Helperland");
        }
        [HttpPost]
        public IActionResult ProviderSignup(User user)
        {
            if (ModelState.IsValid)
            {
                          
           
                var userExists = _helperlandContext.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if (userExists == null)
                {
                    user.UserTypeId = 2;
                    user.CreatedDate = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    user.IsActive = false;
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

            if(user.Email !=null && user.Password != null)
            {

                User credentials = _helperlandContext.Users.Where(x => x.Email == user.Email && x.IsActive == true).FirstOrDefault();
                if(credentials != null)
                {
                bool isvalidpass = BCrypt.Net.BCrypt.Verify(user.Password, credentials.Password);
                if (isvalidpass)
                {
                    HttpContext.Session.SetInt32("userid", credentials.UserId);
                    HttpContext.Session.SetInt32("usertypeid", credentials.UserTypeId);
                    if (credentials.UserTypeId == 1)
                    {
                        return RedirectToAction("Dashboard", "Customer");
                    }   
                    else if (credentials.UserTypeId == 2)
                    {
                        return RedirectToAction("Dashboard", "Provider");
                    }
                    else if (credentials.UserTypeId == 3)
                    {
                        return RedirectToAction("ServiceRequest", "Admin");
                    }
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
                                 "<h1 style=\"color:#051a80;\">Welcome to Helperland. Click Below Button to change your password.</h1> " +
                                  "<a style=\"background:#1d7a8c;padding:9px 20px;color:white;text-decoration:none;font-size:25px;\" href='" + Url.Action("ResetPass", "Account", new { code = resetcode, id = account.UserId }, "http") + "'>Reset Password</a>" +
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
                    TempData["mailsend"] = "send";
                    return Redirect((Url.Action("Index", "Helperland") + "?forgotPass=true"));
                }
            }
            TempData["mailnotfound"] = "NotFound";
            return Redirect((Url.Action("Index", "Helperland") + "?forgotPass=true"));
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
            user.Password = BCrypt.Net.BCrypt.HashPassword(resetpass.password);
            user.ModifiedDate = DateTime.Now;
            _helperlandContext.Users.Update(user);
            _helperlandContext.SaveChanges();
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));

            }
            else
            {
                ViewBag.Resetpasserror = true;
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect((Url.Action("Index", "Helperland") + "?logoutModal=true"));
        }

    }
}
