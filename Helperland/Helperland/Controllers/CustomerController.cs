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
            var userAdd = _helperlandContext.UserAddresses.Where(x => x.UserId == logedUserid && x.PostalCode == EnteredzipCode && x.IsDeleted == false).ToList();

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

        [HttpGet]
        public JsonResult getDashboardDetails()
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            List<CustDashboard> custDashboard = new List<CustDashboard>();

            var ServiceDetail = _helperlandContext.ServiceRequests.Where(x => x.UserId == logedUserid && x.Status == null).ToList();

            foreach (var Service in ServiceDetail)
            {
                CustDashboard RequestData = new CustDashboard();
                RequestData.ServiceId = Service.ServiceRequestId;
                RequestData.ServiceDate = Service.ServiceStartDate.ToString("dd/MM/yyyy");
                RequestData.ServiceStartTime = Service.ServiceStartDate.ToString("HH:mm");
                RequestData.ServiceEndTime = Service.ServiceStartDate.AddHours((double)Service.SubTotal).ToString("HH:mm");
                RequestData.Payment = Service.TotalCost;
                custDashboard.Add(RequestData);
            }
            return new JsonResult(custDashboard);
        }

        //[HttpGet]
        //public ActionResult GetRescheduleRequestData(int ReqServiceId)
        //{
        //    int? logedUserid = HttpContext.Session.GetInt32("userid");
        //    ServiceRequest rescheduleReq = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == Convert.ToInt32(ReqServiceId) && x.UserId == logedUserid).FirstOrDefault();
        //    var ServiceDate = rescheduleReq.ServiceStartDate.ToString("dd/MM/yyyy"); ;
        //    var ServiceStartTime = rescheduleReq.ServiceStartDate.ToString("HH:mm tt");
        //    return Json(ServiceDate + ServiceStartTime); 
        //}

        [HttpGet]
        public ActionResult GetServiceSummaryData(int ReqServiceId)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            ServiceRequest reqService = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == ReqServiceId && x.UserId == logedUserid).FirstOrDefault();
            CustDashServiceSummary custDashServiceSummary = new CustDashServiceSummary();
            custDashServiceSummary.id = ReqServiceId;
            custDashServiceSummary.ServiceDate = reqService.ServiceStartDate.ToString("dd/MM/yyyy");
            custDashServiceSummary.ServiceStartTime = reqService.ServiceStartDate.ToString("HH:mm");
            custDashServiceSummary.ServiceEndTime = reqService.ServiceStartDate.AddHours((double)reqService.SubTotal).ToString("HH:mm");
            custDashServiceSummary.Duration = reqService.SubTotal;
            custDashServiceSummary.Payment = reqService.TotalCost;
            custDashServiceSummary.Comments = reqService.Comments;
            custDashServiceSummary.HavePets = reqService.HasPets;

            ServiceRequestAddress serviceRequestAddress = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == ReqServiceId).FirstOrDefault();
            custDashServiceSummary.AddressLine1 = serviceRequestAddress.AddressLine1;
            custDashServiceSummary.AddressLine2 = serviceRequestAddress.AddressLine2;
            custDashServiceSummary.City = serviceRequestAddress.City;
            custDashServiceSummary.PostalCode = serviceRequestAddress.PostalCode;
            custDashServiceSummary.Mobile = serviceRequestAddress.Mobile;
            custDashServiceSummary.Email = serviceRequestAddress.Email;

            //ServiceRequestExtra serviceRequestExtra = _helperlandContext.ServiceRequestExtras.Where(x => x.ServiceRequestId == ReqServiceId).FirstOrDefault();
            List<ServiceRequestExtra> serviceRequestExtra = _helperlandContext.ServiceRequestExtras.Where(x => x.ServiceRequestId == ReqServiceId).ToList();
            foreach (ServiceRequestExtra row in serviceRequestExtra)
            {
                if (row.ServiceExtraId == 1)
                {
                    custDashServiceSummary.cabinet = true;
                }
                else if (row.ServiceExtraId == 2)
                {
                    custDashServiceSummary.fridge = true;
                }
                else if (row.ServiceExtraId == 3)
                {
                    custDashServiceSummary.oven = true;
                }
                else if (row.ServiceExtraId == 4)
                {
                    custDashServiceSummary.laundary = true;
                }
                else if (row.ServiceExtraId == 5)
                {
                    custDashServiceSummary.window = true;
                }
            }
            

            return Json(custDashServiceSummary);
        }

        [HttpPost]
        public ActionResult RescheduleService(string InputserviceIdVal, string rescheduleServiceTime)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            ServiceRequest rescheduleReq = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == Convert.ToInt32(InputserviceIdVal) && x.UserId == logedUserid).FirstOrDefault();
            rescheduleReq.ServiceStartDate = DateTime.ParseExact(rescheduleServiceTime, "dd/MM/yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            rescheduleReq.ModifiedDate = DateTime.Now;
            _helperlandContext.ServiceRequests.Update(rescheduleReq);
            _helperlandContext.SaveChanges();
            return Json("ok");
        }

        [HttpPost]
        public ActionResult CancleRequest(string InputCancleServiceId, string canclecomments)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            ServiceRequest cancleReq = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == Convert.ToInt32(InputCancleServiceId) && x.UserId == logedUserid).FirstOrDefault();
            cancleReq.Comments = canclecomments;
            cancleReq.Status = 1;
            _helperlandContext.ServiceRequests.Update(cancleReq);
            _helperlandContext.SaveChanges();
            return Json("Successfully");
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

        [HttpGet]
        public JsonResult GetServiceHistoryDetails()
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            List<CustDashboard> custServiceHistory = new List<CustDashboard>();

            var ServiceDetail = _helperlandContext.ServiceRequests.Where(x => x.UserId == logedUserid && x.Status != null).ToList();

            foreach (var Service in ServiceDetail)
            {
                CustDashboard RequestData = new CustDashboard();
                RequestData.ServiceId = Service.ServiceRequestId;
                RequestData.ServiceDate = Service.ServiceStartDate.ToString("dd/MM/yyyy");
                RequestData.ServiceStartTime = Service.ServiceStartDate.ToString("HH:mm");
                RequestData.ServiceEndTime = Service.ServiceStartDate.AddHours((double)Service.SubTotal).ToString("HH:mm");
                RequestData.Payment = Service.TotalCost;
                RequestData.Status = Service.Status;
                custServiceHistory.Add(RequestData);
            }
            return new JsonResult(custServiceHistory);
        }

        public IActionResult MySetting()
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

        [HttpGet]
        public IActionResult getUserDetails(CustMyDetails custMyDetails)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            User credentials = _helperlandContext.Users.Where(x => x.UserId == logedUserid).FirstOrDefault();
            custMyDetails.fname = credentials.FirstName;
            custMyDetails.lname = credentials.LastName;
            custMyDetails.email = credentials.Email;
            custMyDetails.mobile = credentials.Mobile;
            custMyDetails.dob = credentials.DateOfBirth;
            return Json(custMyDetails);
        }

        [HttpPost]

        public ActionResult updateMyDetails(CustMyDetails custMyDetails)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            User reqUser = _helperlandContext.Users.Where(x => x.UserId == logedUserid).FirstOrDefault();
            reqUser.FirstName = custMyDetails.fname;
            reqUser.LastName = custMyDetails.lname;
            reqUser.Mobile = custMyDetails.mobile;
            reqUser.Email = custMyDetails.email;
            reqUser.DateOfBirth = custMyDetails.dob;
            reqUser.ModifiedDate = DateTime.Now;
            reqUser.LanguageId = custMyDetails.languageid;
            _helperlandContext.Users.Update(reqUser);
            _helperlandContext.SaveChanges();
            return Json("updateSuccessfully");
        }

        [HttpGet]
        public JsonResult getUserAddress()
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            List<UserAddressDetails> userAddressDetails = new List<UserAddressDetails>();
            var userAdd = _helperlandContext.UserAddresses.Where(x => x.UserId == logedUserid && x.IsDeleted == false).ToList();
            foreach (var userAddress in userAdd)
            {
                UserAddressDetails RequestAdd = new UserAddressDetails();
                RequestAdd.id = userAddress.AddressId;
                RequestAdd.addressline1 = userAddress.AddressLine1;
                RequestAdd.addressline2 = userAddress.AddressLine2;
                RequestAdd.city = userAddress.City;
                RequestAdd.postalcode = userAddress.PostalCode;
                RequestAdd.mobile = userAddress.Mobile;
                userAddressDetails.Add(RequestAdd);
            }
            return new JsonResult(userAddressDetails);
        }

        [HttpGet]
        public ActionResult EditAddGetReq(int ReqAddId)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            UserAddress userAddress = _helperlandContext.UserAddresses.Where(x => x.UserId == logedUserid && x.AddressId == ReqAddId).FirstOrDefault();
            var add = new
            {
                addline1 = userAddress.AddressLine1,
                addline2 = userAddress.AddressLine2,
                postalcode = userAddress.PostalCode,
                city = userAddress.City,
                mobile = userAddress.Mobile,
            };
            return Json(add);

        }

        [HttpPost]
        public ActionResult EditAddPostReq(UserAddressDetails userAddressDetails)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            UserAddress userAddress = _helperlandContext.UserAddresses.Where(x => x.AddressId == userAddressDetails.id && x.UserId == logedUserid).FirstOrDefault();
            userAddress.AddressLine1 = userAddressDetails.addressline1;
            userAddress.AddressLine2 = userAddressDetails.addressline2;
            userAddress.PostalCode = userAddressDetails.postalcode;
            userAddress.City = userAddressDetails.city;
            userAddress.Mobile = userAddressDetails.mobile;
            _helperlandContext.Update(userAddress);
            _helperlandContext.SaveChanges();
            return Json(userAddress);
        }

        [HttpPost]
        public ActionResult DeleteAddPostReq(int deletAddId)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            UserAddress userAddress = _helperlandContext.UserAddresses.Where(x => x.AddressId == deletAddId && x.UserId == logedUserid).FirstOrDefault();
            userAddress.IsDeleted = true;
            _helperlandContext.UserAddresses.Update(userAddress);
            _helperlandContext.SaveChanges();
            return Json(userAddress);
        }

        [HttpPost]
        public ActionResult AddNewAdd(UserAddressDetails userAddressDetails)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            User reqUser = _helperlandContext.Users.Where(x => x.UserId == logedUserid).FirstOrDefault();

            UserAddress newAddReq = new UserAddress();
            newAddReq.UserId = reqUser.UserId;
            newAddReq.AddressLine1 = userAddressDetails.addressline1;
            newAddReq.AddressLine2 = userAddressDetails.addressline2;
            newAddReq.City = userAddressDetails.city;
            newAddReq.PostalCode = userAddressDetails.postalcode;
            newAddReq.State = userAddressDetails.state;
            newAddReq.Mobile = userAddressDetails.mobile;
            newAddReq.Email = reqUser.Email;
            _helperlandContext.UserAddresses.Add(newAddReq);
            _helperlandContext.SaveChanges();
            return Json("AddSuccessfully");
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
                credentials.ModifiedDate = DateTime.Now;
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
