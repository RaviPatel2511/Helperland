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

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 1 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "Dashboard";
                ViewBag.IsloggedIn = "success";
                ViewBag.UType = 1;
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        public  IActionResult MySetting()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 1 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "Setting";
                ViewBag.IsloggedIn = "success";
                ViewBag.UType = 1;
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        public IActionResult ServiceHistory()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 1 && HttpContext.Session.GetInt32("userid") != null)
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
                if (logedUser.UserTypeId == 1)
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
            if (ModelState.IsValid)
            {

            var availableZipcode = _helperlandContext.Zipcodes.Where(x => x.ZipcodeValue == setupservice.Zipcode).FirstOrDefault();
            if (availableZipcode != null)
            {
                TempData["EnteredZip"] = setupservice.Zipcode;
                    var city = _helperlandContext.Cities.Where(x => x.Id == availableZipcode.CityId).FirstOrDefault();
                    var cityName = city.CityName;
                    return Ok(Json(cityName));
            }
            else
            {
                return Ok(Json("false"));
            }
            }
                return Ok(Json("Invalid"));
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
            TempData.Keep("EnteredZip");
            var userAdd = _helperlandContext.UserAddresses.Where(x => x.UserId == logedUserid && x.PostalCode == EnteredzipCode).ToList();

            foreach (var address in userAdd)
            {
                UserAddressDetails RequestAddress = new UserAddressDetails();
                RequestAddress.id = address.AddressId;
                RequestAddress.addressline1 = address.AddressLine1;
                RequestAddress.addressline2 = address.AddressLine2;
                RequestAddress.city = address.City;
                RequestAddress.mobile = address.Mobile;
                RequestAddress.postalcode = address.PostalCode;
                RequestAddress.IsDefault = address.IsDefault;

                userAddressDetails.Add(RequestAddress);
            }
            return new JsonResult(userAddressDetails);
        }

        [HttpPost]

        public ActionResult AddNewAddressToDB(string AddLine1, string AddLine2, string NewCity, string NewMobile, string NewPostal, UserAddress userAddress)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            if (logedUserid != null)
            {
                var city = _helperlandContext.Cities.Where(x => x.CityName == NewCity).FirstOrDefault();
                var state = _helperlandContext.States.Where(x => x.Id == city.StateId).FirstOrDefault();

                User logedUser = _helperlandContext.Users.Where(x => x.UserId == logedUserid).FirstOrDefault();
                userAddress.UserId = logedUser.UserId;
                userAddress.AddressLine1 = AddLine1;
                userAddress.AddressLine2 = AddLine2;
                userAddress.City = NewCity;
                userAddress.PostalCode = NewPostal;
                userAddress.Mobile = NewMobile;
                userAddress.Email = logedUser.Email;
                userAddress.State = state.StateName;
                userAddress.IsDefault = false;
                userAddress.IsDeleted = false;
                _helperlandContext.UserAddresses.Add(userAddress);
                _helperlandContext.SaveChanges();
                return Ok(Json("true"));
            }
            return Ok(Json("false"));

        }

        [HttpPost]
        public IActionResult CompleteBooking(BookService data)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            if (logedUserid != null)
            {
                ServiceRequest newRequest = new ServiceRequest();
                newRequest.UserId = Convert.ToInt32(logedUserid);
                newRequest.ServiceId = Convert.ToInt32(logedUserid);
                newRequest.ServiceStartDate = DateTime.ParseExact(data.ServiceDateTime, "dd/MM/yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);

                newRequest.ServiceHours = data.ServiceHours;
                newRequest.ExtraHours = data.ExtraServiceHours;
                newRequest.ServiceHourlyRate = 10;
                newRequest.SubTotal = Convert.ToDecimal(newRequest.ServiceHours + newRequest.ExtraHours);
                newRequest.TotalCost = Convert.ToDecimal(newRequest.SubTotal * 10);
                newRequest.Comments = data.Comments;
                newRequest.HasPets = data.HavePets;
                newRequest.CreatedDate = DateTime.Now;
                newRequest.ModifiedDate = DateTime.Now;
                newRequest.ZipCode = data.ServiceZipCode;
                newRequest.PaymentDue = false;
                newRequest.PaymentDone = true;
                newRequest.HasIssue = false;
                newRequest.ModifiedBy = logedUserid;
                var saveBooking = _helperlandContext.ServiceRequests.Add(newRequest);
                _helperlandContext.SaveChanges();

                
                UserAddress selectedAddress = _helperlandContext.UserAddresses.Where(x => x.AddressId == data.SelectedAddressId).FirstOrDefault();

                ServiceRequestAddress newBookingAddress = new ServiceRequestAddress();
                newBookingAddress.ServiceRequestId = saveBooking.Entity.ServiceRequestId;
                newBookingAddress.AddressLine1 = selectedAddress.AddressLine1;
                newBookingAddress.AddressLine2 = selectedAddress.AddressLine2;
                newBookingAddress.City = selectedAddress.City;
                newBookingAddress.State = selectedAddress.State; // no any input
                newBookingAddress.PostalCode = selectedAddress.PostalCode;
                newBookingAddress.Mobile = selectedAddress.Mobile;
                newBookingAddress.Email = selectedAddress.Email;
                newBookingAddress.State = selectedAddress.State;
                var saveaddress = _helperlandContext.ServiceRequestAddresses.Add(newBookingAddress);
                _helperlandContext.SaveChanges();



                if (data.cabinet)
                {
                    ServiceRequestExtra newExtraService = new ServiceRequestExtra();
                    newExtraService.ServiceRequestId = saveBooking.Entity.ServiceRequestId;
                    newExtraService.ServiceExtraId = 1;
                    _helperlandContext.ServiceRequestExtras.Add(newExtraService);
                    _helperlandContext.SaveChanges();
                }
                if (data.fridge)
                {
                    ServiceRequestExtra newExtraService = new ServiceRequestExtra();
                    newExtraService.ServiceRequestId = saveBooking.Entity.ServiceRequestId;
                    newExtraService.ServiceExtraId = 2;
                    _helperlandContext.ServiceRequestExtras.Add(newExtraService);
                    _helperlandContext.SaveChanges();
                }
                if (data.oven)
                {
                    ServiceRequestExtra newExtraService = new ServiceRequestExtra();
                    newExtraService.ServiceRequestId = saveBooking.Entity.ServiceRequestId;
                    newExtraService.ServiceExtraId = 3;
                    _helperlandContext.ServiceRequestExtras.Add(newExtraService);
                    _helperlandContext.SaveChanges();
                }
                if (data.laundary)
                {
                    ServiceRequestExtra newExtraService = new ServiceRequestExtra();
                    newExtraService.ServiceRequestId = saveBooking.Entity.ServiceRequestId;
                    newExtraService.ServiceExtraId = 4;
                    _helperlandContext.ServiceRequestExtras.Add(newExtraService);
                    _helperlandContext.SaveChanges();
                }
                if (data.window)
                {
                    ServiceRequestExtra newExtraService = new ServiceRequestExtra();
                    newExtraService.ServiceRequestId = saveBooking.Entity.ServiceRequestId;
                    newExtraService.ServiceExtraId = 5;
                    _helperlandContext.ServiceRequestExtras.Add(newExtraService);
                    _helperlandContext.SaveChanges();
                }

                return Json(saveBooking.Entity.ServiceRequestId);
            }
            return Json("false");
        }

        [HttpGet]
        public JsonResult getDashboardDetails()
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            List<CustDashboard> custDashboard = new List<CustDashboard>();

            var ServiceDetail = _helperlandContext.ServiceRequests.Where(x => x.UserId == logedUserid).ToList();

            foreach (var Service in ServiceDetail)
            {
                CustDashboard RequestData = new CustDashboard();
                RequestData.ServiceId = Service.ServiceRequestId;
                RequestData.ServiceDate = Service.ServiceStartDate;
                RequestData.Payment = Service.TotalCost;
                custDashboard.Add(RequestData);
            }
            return new JsonResult(custDashboard);
        }

        [HttpPost]

        public IActionResult UpdatePassword(ResetPass resetPass)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            User credentials = _helperlandContext.Users.Where(x => x.UserId == logedUserid).FirstOrDefault();
            bool isvalidpass = BCrypt.Net.BCrypt.Verify(resetPass.oldPassword, credentials.Password);
            if (isvalidpass)
            {
                credentials.Password = BCrypt.Net.BCrypt.HashPassword(resetPass.password);
                _helperlandContext.Users.Update(credentials);
                _helperlandContext.SaveChanges();
                return Json("PasswordUpdate");
            }
            else
            {
                return Json("wrongPassword");
            }
        }
    }
}
