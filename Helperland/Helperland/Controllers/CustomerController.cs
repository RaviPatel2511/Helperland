using Helperland.Models;
using Helperland.Models.Data;
using Helperland.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HelperlandContext _helperlandContext;
        public CustomerController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
        public IActionResult ServiceHistory()
        {
            if(HttpContext.Session.GetInt32("usertypeid") == 1 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "ServiceHistory";
                ViewBag.IsloggedIn = "success";
                ViewBag.UType = 1;
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
           return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        public IActionResult BookService()
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            if (logedUserid == null)
            {
                return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
            }
            else
            {
                User logedUser = _helperlandContext.Users.Where(x => x.UserId == logedUserid).FirstOrDefault();
                ViewBag.IsloggedIn = "success";
                ViewBag.UserName = logedUser.FirstName;
                ViewBag.UType = logedUser.UserTypeId;
                ViewBag.Title = "BookService";
                if(logedUser.UserTypeId == 1)
                {

                return View();
                }
                else
                {
                    return RedirectToAction("Error", "Helperland");
                }

            }
               

        }
        
        [HttpPost]
        public ActionResult IsAvailableZip(setupService setupservice)
        {
            var availableZipcode = _helperlandContext.Zipcodes.Where(x => x.ZipcodeValue == setupservice.Zipcode);
            if(availableZipcode.Count() > 0)
            {
                TempData["EnteredZip"] = setupservice.Zipcode;
                return Ok(Json("true"));
            }
            else
            {
                return Ok(Json("false"));
            }
         }

        [HttpPost]
        public ActionResult ScheduleService(ScheduleService scheduleService)
        {
            if (ModelState.IsValid)
            {
                return Ok(Json("true"));
            }
            else
            {
                return Ok(Json("false"));
            }
        }

        [HttpGet]

        public JsonResult getAddressOfUser()
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            List<UserAddressDetails> userAddressDetails = new List<UserAddressDetails>();

            var EnteredzipCode = Convert.ToString(TempData["EnteredZip"]);
            var userAdd = _helperlandContext.UserAddresses.Where(x=>x.UserId == logedUserid && x.PostalCode == EnteredzipCode).ToList();

            foreach (var address in userAdd)
            {
                UserAddressDetails RequestAddress = new UserAddressDetails();
                RequestAddress.id = address.AddressId;
                RequestAddress.addressline1= address.AddressLine1;
                RequestAddress.addressline2 = address.AddressLine2;
                RequestAddress.city = address.City;
                RequestAddress.mobile = address.Mobile;
                RequestAddress.postalcode = address.PostalCode;
                RequestAddress.IsDefault = address.IsDefault;

                userAddressDetails.Add(RequestAddress);
            }

            return new JsonResult(userAddressDetails);


        }
    }
}
