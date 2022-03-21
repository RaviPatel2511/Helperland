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
    public class CustomerController : Controller
    {
        private readonly HelperlandContext _helperlandContext;
        public CustomerController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
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
            TempData.Keep("EnteredZip");
            return new JsonResult(userAddressDetails);
        }

        [HttpGet]

        public ActionResult GetFavPro()
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            List<FavPro> FavProList = new List<FavPro>();
            var EnteredzipCode = Convert.ToString(TempData["EnteredZip"]);
            var FavProData = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == logedUserid && x.IsFavorite == true && x.IsBlocked == false).ToList();
            foreach(var data in FavProData)
            {
                FavPro favPro = new FavPro();
                User user = _helperlandContext.Users.Where(x => x.UserId == data.TargetUserId && x.ZipCode == EnteredzipCode).FirstOrDefault();
                if (user != null)
                {
                        favPro.ProId = user.UserId;
                        favPro.Name = user.FirstName + " " + user.LastName;
                        if (user.UserProfilePicture != null)
                        {
                            favPro.Avtar = user.UserProfilePicture;
                        }
                        else
                        {
                            favPro.Avtar = "cap";
                        }
                    FavProList.Add(favPro);
                }
                
            }
            TempData.Keep("EnteredZip");
            return new JsonResult(FavProList);
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
                if (data.FavProId != null)
                {

                    var ProAllReq = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == data.FavProId && x.Status == null).ToList();
                    if (ProAllReq.Count() > 0)
                    {
                        var HaveService = false;


                        foreach (var i in ProAllReq)
                        {
                            var NewStartDate = DateTime.ParseExact(data.ServiceDateTime, "dd/MM/yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                            var NewEndDate = DateTime.ParseExact(data.ServiceDateTime, "dd/MM/yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture).AddHours((double)(newRequest.ServiceHours + newRequest.ExtraHours));
                            var HaveStartDate = i.ServiceStartDate;
                            var HaveEndDate = i.ServiceStartDate.AddHours((double)i.SubTotal + 1);

                            if (NewStartDate >= HaveStartDate && NewStartDate <= HaveEndDate)
                            {
                                HaveService = true;
                                break;
                            }
                            else if (NewEndDate >= HaveStartDate && NewEndDate <= HaveEndDate)
                            {
                                HaveService = true;
                                break;

                            }
                            else if (NewStartDate < HaveStartDate && NewEndDate > HaveEndDate)
                            {
                                HaveService = true;
                                break;
                            }
                        }
                        if (HaveService)
                        {
                            return Json("AnotherServiceBooked");
                        }
                        newRequest.ServiceProviderId = data.FavProId;
                        newRequest.SpacceptedDate = DateTime.Now;
                    }
                        
                }
                var saveBooking = _helperlandContext.ServiceRequests.Add(newRequest);
                _helperlandContext.SaveChanges();

                
                UserAddress selectedAddress = _helperlandContext.UserAddresses.Where(x => x.AddressId == data.SelectedAddressId).FirstOrDefault();

                ServiceRequestAddress newBookingAddress = new ServiceRequestAddress();
                newBookingAddress.ServiceRequestId = saveBooking.Entity.ServiceRequestId;
                newBookingAddress.AddressLine1 = selectedAddress.AddressLine1;
                newBookingAddress.AddressLine2 = selectedAddress.AddressLine2;
                newBookingAddress.City = selectedAddress.City;
                newBookingAddress.State = selectedAddress.State;
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

                if (data.FavProId != null)
                {
                    var PerticularProvider = _helperlandContext.Users.Where(x => x.UserId == data.FavProId).FirstOrDefault();

                        string subject = "A new service booking request has arrived in your area .";
                        string mailTitle = "Helperland Service";
                        string fromEmail = "codewithravi2511@gmail.com";
                        string fromEmailPassword = "dyto qxph hvgv oslf";


                        string MailBody = "<!DOCTYPE html>" +
                                 "<html> " +
                                     "<body style=\"background -color:#ff7f26;text-align:center;\"> " +
                                     "<h1 style=\"color:#051a80;\">Welcome to Helperland.</h1> " +
                                     "<p>Dear " + PerticularProvider.FirstName + " " + PerticularProvider.LastName + " ,</p>" +
                                      "<p>A new service has been booked in your area with reference id " + saveBooking.Entity.ServiceRequestId + " , </p>" +
                                      "<p>For more information of service or to accept the service please Login to your account</p>" +
                                      "<a style=\"background:#1d7a8c;padding:5px 10px;color:white;text-decoration:none;font-size:25px;\"  href='" + Url.Action("Index", "Helperland", new { }, "http") + "'>Login Now</a>" +
                                     "</body> " +
                                 "</html>";
                        MailMessage message = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress(PerticularProvider.Email));
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
                }
                else
                {
                    var AvailableProvider = _helperlandContext.Users.Where(x => x.ZipCode == data.ServiceZipCode).ToList();

                    foreach (var availPro in AvailableProvider)
                    {
                        FavoriteAndBlocked blockPro = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == logedUserid && x.TargetUserId == availPro.UserId && x.IsBlocked == true).FirstOrDefault();
                        if (blockPro == null)
                        {
                            string subject = "A new service booking request has arrived in your area .";
                            string mailTitle = "Helperland Service";
                            string fromEmail = "codewithravi2511@gmail.com";
                            string fromEmailPassword = "dyto qxph hvgv oslf";


                            string MailBody = "<!DOCTYPE html>" +
                                     "<html> " +
                                         "<body style=\"background -color:#ff7f26;text-align:center;\"> " +
                                         "<h1 style=\"color:#051a80;\">Welcome to Helperland.</h1> " +
                                         "<p>Dear " + availPro.FirstName + " " + availPro.LastName + " ,</p>" +
                                          "<p>A new service has been booked in your area with reference id " + saveBooking.Entity.ServiceRequestId + " , </p>" +
                                          "<p>For more information of service or to accept the service please Login to your account</p>" +
                                          "<a style=\"background:#1d7a8c;padding:5px 10px;color:white;text-decoration:none;font-size:25px;\"  href='" + Url.Action("Index", "Helperland", new { }, "http") + "'>Login Now</a>" +
                                         "</body> " +
                                     "</html>";
                            MailMessage message = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress(availPro.Email));
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
                        }
                        
                    }
                }
                return Json(saveBooking.Entity.ServiceRequestId);

            }
            return Json("false");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
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
                RequestData.ProviderId = Service.ServiceProviderId;
                if(Service.ServiceProviderId != null)
                {
                User user = _helperlandContext.Users.Where(x => x.UserId == Service.ServiceProviderId).FirstOrDefault();
                RequestData.Spname = user.FirstName + " " + user.LastName;
                    if (user.UserProfilePicture != null)
                    {
                        RequestData.Avtar = user.UserProfilePicture;
                    }
                var rating = _helperlandContext.Ratings.Where(x => x.RatingTo == Service.ServiceProviderId);
                if (rating.Count() > 0)
                {
                    RequestData.SpRatings = Math.Round(rating.Average(x => x.Ratings), 1);
                }
                else
                {
                    RequestData.SpRatings = 0;
                }
                }
                custDashboard.Add(RequestData);
            }
            return new JsonResult(custDashboard);
        }

        [HttpGet]
        public ActionResult GetRescheduleRequestData(int ReqServiceId)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            ServiceRequest rescheduleReq = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == Convert.ToInt32(ReqServiceId) && x.UserId == logedUserid).FirstOrDefault();
            var ServiceDate = rescheduleReq.ServiceStartDate.ToString("dd/MM/yyyy");
            var ServiceStartTime = rescheduleReq.ServiceStartDate.ToString("HH:mm");
            var obj = new
            {
                serviceDate = ServiceDate,
                serviceStartTime = ServiceStartTime
            };
            return Json(obj);
        }

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
            ServiceRequest rescheduleReq = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == Convert.ToInt32(InputserviceIdVal)).FirstOrDefault();
            if(rescheduleReq.ServiceProviderId == null)
            {
                rescheduleReq.ServiceStartDate = DateTime.ParseExact(rescheduleServiceTime, "dd/MM/yyyy HH:mm", null);
                rescheduleReq.ModifiedDate = DateTime.Now;
                _helperlandContext.ServiceRequests.Update(rescheduleReq);
                _helperlandContext.SaveChanges();
                return Json("ok");
            }
            else
            {
                User AssignProvider = _helperlandContext.Users.Where(x => x.UserId == rescheduleReq.ServiceProviderId).FirstOrDefault();
                var ProAllReq = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == rescheduleReq.ServiceProviderId && x.Status == null && x.ServiceRequestId != rescheduleReq.ServiceRequestId).ToList();
                if (ProAllReq.Count() > 0)
                {
                    var HaveService = false;
                   

                   foreach(var i in ProAllReq)
                    {
                        var NewStartDate = DateTime.ParseExact(rescheduleServiceTime, "dd/MM/yyyy HH:mm", null);
                        var NewEndDate = DateTime.ParseExact(rescheduleServiceTime, "dd/MM/yyyy HH:mm", null).AddHours((double)rescheduleReq.SubTotal);
                        var HaveStartDate = i.ServiceStartDate;
                        var HaveEndDate = i.ServiceStartDate.AddHours((double)i.SubTotal + 1);

                        if (NewStartDate >= HaveStartDate && NewStartDate <= HaveEndDate)
                        {
                            HaveService = true;
                            break;
                        }
                        else if (NewEndDate >= HaveStartDate && NewEndDate <= HaveEndDate)
                        {
                            HaveService = true;
                            break;

                        }
                        else if (NewStartDate < HaveStartDate && NewEndDate > HaveEndDate)
                        {
                            HaveService = true;
                            break;
                        }
                    }
                    if (HaveService)
                    {
                        return Json("AnotherServiceBooked");
                    }
                    else
                    {
                        rescheduleReq.ServiceStartDate = DateTime.ParseExact(rescheduleServiceTime, "dd/MM/yyyy HH:mm", null);
                        rescheduleReq.ModifiedDate = DateTime.Now;
                        _helperlandContext.ServiceRequests.Update(rescheduleReq);
                        _helperlandContext.SaveChanges();

                        User AssignProvider3 = _helperlandContext.Users.Where(x => x.UserId == rescheduleReq.ServiceProviderId).FirstOrDefault();
                        string subject = "Customer Has Changed service date or time.";
                        string mailTitle = "Helperland Service";
                        string fromEmail = "codewithravi2511@gmail.com";
                        string fromEmailPassword = "dyto qxph hvgv oslf";

                        string MailBody = "<!DOCTYPE html>" +
                                     "<html> " +
                                         "<body style=\"background -color:#ff7f26;text-align:center;\"> " +
                                         "<h1 style=\"color:#051a80;\">Welcome to Helperland.</h1> " +
                                         "<p>Dear " + AssignProvider3.FirstName + " " + AssignProvider3.LastName + " ,</p>" +
                                          "<p>Service Request " + rescheduleReq.ServiceRequestId + " has been rescheduled by customer. New date and time are " + DateTime.ParseExact(rescheduleServiceTime, "dd/MM/yyyy HH:mm", null) + ",</p>" +
                                          "<p>For more information of service please Login to your account</p>" +
                                          "<a style=\"background:#1d7a8c;padding:5px 10px;color:white;text-decoration:none;font-size:25px;\"  href='" + Url.Action("Index", "Helperland", new { }, "http") + "'>Login Now</a>" +
                                         "</body> " +
                                     "</html>";
                        MailMessage message = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress(AssignProvider3.Email));
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
                        return Json("ok");
                    }
                }
                else
                {
                    rescheduleReq.ServiceStartDate = DateTime.ParseExact(rescheduleServiceTime, "dd/MM/yyyy HH:mm", null);
                    rescheduleReq.ModifiedDate = DateTime.Now;
                    _helperlandContext.ServiceRequests.Update(rescheduleReq);
                    _helperlandContext.SaveChanges();

                    User AssignProvider2 = _helperlandContext.Users.Where(x => x.UserId == rescheduleReq.ServiceProviderId).FirstOrDefault();
                    string subject = "Customer Has Changed service date or time.";
                    string mailTitle = "Helperland Service";
                    string fromEmail = "codewithravi2511@gmail.com";
                    string fromEmailPassword = "dyto qxph hvgv oslf";

                        string MailBody = "<!DOCTYPE html>" +
                                     "<html> " +
                                         "<body style=\"background -color:#ff7f26;text-align:center;\"> " +
                                         "<h1 style=\"color:#051a80;\">Welcome to Helperland.</h1> " +
                                         "<p>Dear " + AssignProvider2.FirstName + " " + AssignProvider2.LastName + " ,</p>" +
                                          "<p>Service Request " + rescheduleReq.ServiceRequestId + " has been rescheduled by customer. New date and time are " + DateTime.ParseExact(rescheduleServiceTime, "dd/MM/yyyy HH:mm", null) + ",</p>" +
                                          "<p>For more information of service please Login to your account</p>" +
                                          "<a style=\"background:#1d7a8c;padding:5px 10px;color:white;text-decoration:none;font-size:25px;\"  href='" + Url.Action("Index", "Helperland", new { }, "http") + "'>Login Now</a>" +
                                         "</body> " +
                                     "</html>";
                        MailMessage message = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress(AssignProvider2.Email));
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
                        return Json("ok");

                }
            }
        }

        [HttpPost]
        public ActionResult CancleRequest(string InputCancleServiceId, string canclecomments)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            ServiceRequest cancleReq = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == Convert.ToInt32(InputCancleServiceId) && x.UserId == logedUserid).FirstOrDefault();
            cancleReq.Comments = canclecomments;
            cancleReq.Status = 1;
            cancleReq.ModifiedBy = logedUserid;
            cancleReq.ModifiedDate = DateTime.Now;
            _helperlandContext.ServiceRequests.Update(cancleReq);
            _helperlandContext.SaveChanges();



            string subject = "Customer Has Cancelled Service.";
            string mailTitle = "Helperland Service";
            string fromEmail = "codewithravi2511@gmail.com";
            string fromEmailPassword = "dyto qxph hvgv oslf";

            if (cancleReq.ServiceProviderId != null)
            {
            User AssignProvider = _helperlandContext.Users.Where(x => x.UserId == cancleReq.ServiceProviderId).FirstOrDefault();
                string MailBody = "<!DOCTYPE html>" +
                             "<html> " +
                                 "<body style=\"background -color:#ff7f26;text-align:center;\"> " +
                                 "<h1 style=\"color:#051a80;\">Welcome to Helperland.</h1> " +
                                 "<p>Dear " + AssignProvider.FirstName + " " + AssignProvider.LastName + " ,</p>" +
                                  "<p>Service Request " + cancleReq.ServiceRequestId + " has been cancelled by customer ,</p>" +
                                  "<p>For more information of service please Login to your account</p>" +
                                  "<a style=\"background:#1d7a8c;padding:5px 10px;color:white;text-decoration:none;font-size:25px;\"  href='" + Url.Action("Index", "Helperland", new { }, "http") + "'>Login Now</a>" +
                                 "</body> " +
                             "</html>";
                MailMessage message = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress(AssignProvider.Email));
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
            }


            return Json("Successfully");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult DashServiceSchedule()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 1 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "Service Schedule";
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
        public ActionResult GetServiceScheduleData()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 1 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                List<CustDashboard> ServiceSchedule = new List<CustDashboard>();
                var Alldata = _helperlandContext.ServiceRequests.Where(x => x.UserId == logedUserid && x.Status != 1).ToList();
                if (Alldata != null)
                {
                    foreach (var data in Alldata)
                    {
                        CustDashboard ServiceScheduleData = new CustDashboard();
                        ServiceScheduleData.ServiceId = data.ServiceRequestId;
                        ServiceScheduleData.ServiceDate = data.ServiceStartDate.ToString("dd/MM/yyyy");
                        ServiceScheduleData.ServiceStartTime = data.ServiceStartDate.ToString("HH:mm");
                        ServiceScheduleData.ServiceEndTime = data.ServiceStartDate.AddHours((double)data.SubTotal).ToString("HH:mm");
                        if (data.Status == null)
                        {
                            ServiceScheduleData.Color = "#1d7a8c";
                        }
                        else
                        {
                            ServiceScheduleData.Color = "#86858b";
                        }
                        ServiceSchedule.Add(ServiceScheduleData);
                    }
                    return new JsonResult(ServiceSchedule);
                }

            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult FavPro()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 1 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "Favourite Provider";
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

        public ActionResult GetFavouritePro()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 1 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");

                List<FavPro> FavouritePro = new List<FavPro>();

                List<int?> AllReq = _helperlandContext.ServiceRequests.Where(x => x.UserId == logedUserid && x.Status == 2).Select(x => x.ServiceProviderId).Distinct().ToList();
                if (AllReq.Count() > 0)
                {
                    foreach (var req in AllReq)
                    {
                        FavPro FavProData = new FavPro();
                        FavProData.ProId = req;

                        var ProviderData = _helperlandContext.Users.Where(x => x.UserId == req).FirstOrDefault();
                        FavProData.Name = ProviderData.FirstName + " " + ProviderData.LastName;
                        var count = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == req && x.Status == 2).Count();
                        FavProData.TotalCleaning = count;
                        if (ProviderData.UserProfilePicture != null)
                        {
                            FavProData.Avtar = ProviderData.UserProfilePicture;
                        }
                        else
                        {
                            FavProData.Avtar = "cap";
                        }
                        var rating = _helperlandContext.Ratings.Where(x => x.RatingTo == ProviderData.UserId);
                        if (rating.Count() > 0)
                        {
                            FavProData.SpRating = Math.Round(rating.Average(x => x.Ratings), 1);
                        }
                        else
                        {
                            FavProData.SpRating = 0;
                        }

                        var isBlocked = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == logedUserid && x.TargetUserId == req).FirstOrDefault();

                        if (isBlocked != null)
                        {
                            if (isBlocked.IsBlocked == true)
                            {
                                FavProData.IsBlocked = true;
                            }
                            else
                            {
                                FavProData.IsBlocked = false;
                            }

                            if(isBlocked.IsFavorite == true)
                            {
                                FavProData.IsFavourite = true;
                            }
                            else
                            {
                                FavProData.IsFavourite = false;
                            }
                        }
                        else
                        {
                            FavProData.IsBlocked = false;
                            FavProData.IsFavourite = false;
                        }
                        FavouritePro.Add(FavProData);
                    }
                    return new JsonResult(FavouritePro);
                }
                else
                {
                    return Json("noData");
                }
            }

            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpPost]
        public IActionResult PostBlockProvider(int ProId)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 1 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                if (_helperlandContext.FavoriteAndBlockeds.Any(x => x.UserId == logedUserid && x.TargetUserId == ProId))
                {
                    FavoriteAndBlocked req = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == logedUserid && x.TargetUserId == ProId).FirstOrDefault();
                    req.IsBlocked = true;

                    _helperlandContext.FavoriteAndBlockeds.Update(req);
                    _helperlandContext.SaveChanges();
                }
                else
                {
                    FavoriteAndBlocked req = new FavoriteAndBlocked();
                    req.UserId = Convert.ToInt32(logedUserid);
                    req.TargetUserId = ProId;
                    req.IsFavorite = false;
                    req.IsBlocked = true;

                    _helperlandContext.FavoriteAndBlockeds.Add(req);
                    _helperlandContext.SaveChanges();
                }
                return Json("success");
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpPost]
        public IActionResult UnBlockProvider(int ProId)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 1 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                FavoriteAndBlocked req = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == logedUserid && x.TargetUserId == ProId).FirstOrDefault();
                req.IsBlocked = false;
                _helperlandContext.FavoriteAndBlockeds.Update(req);
                _helperlandContext.SaveChanges();
                return Json("success");
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
        [HttpPost]
        public IActionResult FavProvider(int ProId)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 1 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                if (_helperlandContext.FavoriteAndBlockeds.Any(x => x.UserId == logedUserid && x.TargetUserId == ProId))
                {
                    FavoriteAndBlocked req = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == logedUserid && x.TargetUserId == ProId).FirstOrDefault();
                    req.IsFavorite = true;

                    _helperlandContext.FavoriteAndBlockeds.Update(req);
                    _helperlandContext.SaveChanges();
                }
                else
                {
                    FavoriteAndBlocked req = new FavoriteAndBlocked();
                    req.UserId = Convert.ToInt32(logedUserid);
                    req.TargetUserId = ProId;
                    req.IsFavorite = true;
                    req.IsBlocked = false;

                    _helperlandContext.FavoriteAndBlockeds.Add(req);
                    _helperlandContext.SaveChanges();
                }
                return Json("success");
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpPost]
        public IActionResult UnfavProvider(int ProId)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 1 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                FavoriteAndBlocked req = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == logedUserid && x.TargetUserId == ProId).FirstOrDefault();
                req.IsFavorite = false;
                _helperlandContext.FavoriteAndBlockeds.Update(req);
                _helperlandContext.SaveChanges();
                return Json("success");
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
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
                RequestData.ProviderId = Service.ServiceProviderId;
                if (Service.ServiceProviderId != null)
                {
                    User user = _helperlandContext.Users.Where(x => x.UserId == Service.ServiceProviderId).FirstOrDefault();
                    var rating = _helperlandContext.Ratings.Where(x => x.RatingTo == Service.ServiceProviderId);
                    if (rating.Count() > 0)
                    {
                        RequestData.SpRatings = Math.Round(rating.Average(x => x.Ratings), 1);
                    }
                    else
                    {
                        RequestData.SpRatings = 0;
                    }
                    RequestData.Spname = user.FirstName + " " + user.LastName;
                    if (user.UserProfilePicture != null)
                    {
                        RequestData.Avtar = user.UserProfilePicture;
                    }
                }
                custServiceHistory.Add(RequestData);
            }
            return new JsonResult(custServiceHistory);
        }

        [HttpGet]
        public ActionResult GetRatingData(int ReqServiceId)
        {
            Rating rating = _helperlandContext.Ratings.Where(x => x.ServiceRequestId == ReqServiceId).FirstOrDefault();
            if(rating != null)
            {
                var obj = new
                {
                    ontime = rating.OnTimeArrival,
                    friendly = rating.Friendly,
                    quality = rating.QualityOfService,
                    comment = rating.Comments
                };
                return Json(obj);
            }
            else
            {
                return Json("NoRatingFound");
            }
        }

        [HttpPost]
        public ActionResult SaveSpRating(SpRating spRating)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            ServiceRequest ServiceDetail = _helperlandContext.ServiceRequests.Where(x => x.UserId == logedUserid && x.ServiceRequestId == spRating.ServiceRequestId).FirstOrDefault();

            Rating rating = new Rating();
            rating.ServiceRequestId = ServiceDetail.ServiceRequestId;
            rating.RatingFrom = ServiceDetail.UserId;
            rating.RatingTo = (int)ServiceDetail.ServiceProviderId;
            rating.Comments = spRating.Comments;
            rating.OnTimeArrival = spRating.OnTimeArrival;
            rating.Friendly = spRating.Friendly;
            rating.QualityOfService = spRating.QualityOfService;
            rating.Ratings = (spRating.OnTimeArrival + spRating.Friendly + spRating.QualityOfService)/3;
            rating.RatingDate = DateTime.Now;
            _helperlandContext.Ratings.Add(rating);
            _helperlandContext.SaveChanges();
            return Json("Successfully");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
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
