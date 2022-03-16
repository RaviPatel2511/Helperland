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
    public class ProviderController : Controller
    {
        private readonly HelperlandContext _helperlandContext;
        public ProviderController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "Dashboard";
                ViewBag.IsloggedIn = "success";
                ViewBag.UType = 2;
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpGet]
        public IActionResult GetDashboardData()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                var ProviderZipcode = _helperlandContext.Users.FirstOrDefault(x => x.UserId == logedUserid).ZipCode;
                var NewService = _helperlandContext.ServiceRequests.Where(x => x.ZipCode == ProviderZipcode && x.ServiceProviderId == null && x.Status == null).ToList();
                int NewServicveCount = 0;
                foreach (var item in NewService)
                {
                    if (!_helperlandContext.FavoriteAndBlockeds.Any(x => x.UserId == logedUserid && x.TargetUserId == item.UserId && x.IsBlocked == true))
                    {
                        NewServicveCount += 1;
                    }

                }
                var UpcomingService = _helperlandContext.ServiceRequests.Where(x => x.ZipCode == ProviderZipcode && x.ServiceProviderId == logedUserid && x.Status == null).ToList().Count();
                var CompleteService = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == logedUserid && x.Status == 2).ToList().Count();

                var obj = new
                {
                    TotalNewService = NewServicveCount,
                    TotalUpcomingService = UpcomingService,
                    TotalCompleteService = CompleteService
                };

                return Json(obj);
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
        public IActionResult MySetting()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "My Setting";
                ViewBag.IsloggedIn = "success";
                ViewBag.UType = 2;
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpGet]
        public IActionResult getUserDetails(ProMyDetails proMyDetails)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            User user = _helperlandContext.Users.Where(x => x.UserId == logedUserid).FirstOrDefault();
            proMyDetails.fname = user.FirstName;
            proMyDetails.lname = user.LastName;
            proMyDetails.email = user.Email;
            proMyDetails.mobile = user.Mobile;
            proMyDetails.dob = user.DateOfBirth;
            proMyDetails.nationnalityId = user.NationalityId;
            proMyDetails.gender = user.Gender;
            proMyDetails.avtar = user.UserProfilePicture;
            proMyDetails.IsActive = user.IsActive;
            UserAddress userAddress = _helperlandContext.UserAddresses.Where(x => x.UserId == logedUserid).FirstOrDefault();
                if (userAddress != null)
                {
                    proMyDetails.addressline1 = userAddress.AddressLine1;
                    proMyDetails.addressline2 = userAddress.AddressLine2;
                    proMyDetails.city = userAddress.City;
                    proMyDetails.postalcode = userAddress.PostalCode;
                    proMyDetails.state = userAddress.State;
                }
            
            return Json(proMyDetails);
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpPost]
        public ActionResult updateMyDetails(ProMyDetails proMyDetails)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                User user = _helperlandContext.Users.Where(x => x.UserId == logedUserid).FirstOrDefault();
                user.FirstName = proMyDetails.fname;
                user.LastName = proMyDetails.lname;
                user.Mobile = proMyDetails.mobile;
                user.DateOfBirth = proMyDetails.dob;
                user.NationalityId = proMyDetails.nationnalityId;
                user.Gender = proMyDetails.gender;
                user.UserProfilePicture = proMyDetails.avtar;
                user.ModifiedBy = Convert.ToInt32(logedUserid);
                user.ModifiedDate = DateTime.Now;
                user.ZipCode = proMyDetails.postalcode;
                _helperlandContext.Users.Update(user);
                _helperlandContext.SaveChanges();

                UserAddress userAddress = _helperlandContext.UserAddresses.Where(x => x.UserId == logedUserid).FirstOrDefault();
                if (userAddress == null)
                {
                    UserAddress newAdd = new UserAddress();
                    newAdd.UserId = Convert.ToInt32(logedUserid);
                    newAdd.AddressLine1 = proMyDetails.addressline1;
                    newAdd.AddressLine2 = proMyDetails.addressline2;
                    newAdd.City = proMyDetails.city;
                    newAdd.State = proMyDetails.state;
                    newAdd.PostalCode = proMyDetails.postalcode;
                    newAdd.Mobile = proMyDetails.mobile;
                    newAdd.Email = proMyDetails.email;
                    _helperlandContext.UserAddresses.Add(newAdd);
                    _helperlandContext.SaveChanges();

                    State state = _helperlandContext.States.Where(x => x.StateName == proMyDetails.state).FirstOrDefault();
                    if(state == null)
                    {
                        State newState = new State();
                        newState.StateName = proMyDetails.state;
                        _helperlandContext.States.Add(newState);
                        _helperlandContext.SaveChanges();
                    }

                    City city = _helperlandContext.Cities.Where(x => x.CityName == proMyDetails.city).FirstOrDefault();
                    if(city == null)
                    {
                        City newCity = new City();
                        State stateId = _helperlandContext.States.Where(x => x.StateName == proMyDetails.state).FirstOrDefault();
                        newCity.CityName = proMyDetails.city;
                        newCity.StateId = stateId.Id;
                        _helperlandContext.Cities.Add(newCity);
                        _helperlandContext.SaveChanges();
                    }

                    Zipcode newzip = new Zipcode();
                    newzip.ZipcodeValue = proMyDetails.postalcode;
                    City cityId = _helperlandContext.Cities.Where(x => x.CityName == proMyDetails.city).FirstOrDefault();
                    newzip.CityId = cityId.Id;
                    _helperlandContext.Zipcodes.Add(newzip);
                    _helperlandContext.SaveChanges();

                    //return Json("Update");
                }
                else
                {
                    State state = _helperlandContext.States.Where(x => x.StateName == proMyDetails.state).FirstOrDefault();
                    if (state == null)
                    {
                        State newState = new State();
                        newState.StateName = proMyDetails.state;
                        _helperlandContext.States.Add(newState);
                        _helperlandContext.SaveChanges();
                    }

                    City city = _helperlandContext.Cities.Where(x => x.CityName == proMyDetails.city).FirstOrDefault();
                    if (city == null)
                    {
                        City newCity = new City();
                        State stateId = _helperlandContext.States.Where(x => x.StateName == proMyDetails.state).FirstOrDefault();
                        newCity.CityName = proMyDetails.city;
                        newCity.StateId = stateId.Id;
                        _helperlandContext.Cities.Add(newCity);
                        _helperlandContext.SaveChanges();
                    }

                    Zipcode zipcode = _helperlandContext.Zipcodes.Where(x => x.ZipcodeValue == userAddress.PostalCode).FirstOrDefault();
                    if(zipcode != null)
                    {
                        zipcode.ZipcodeValue = proMyDetails.postalcode;
                        City cityId = _helperlandContext.Cities.Where(x => x.CityName == proMyDetails.city).FirstOrDefault();
                        zipcode.CityId = cityId.Id;
                        _helperlandContext.Zipcodes.Update(zipcode);
                        _helperlandContext.SaveChanges();
                    }
                    else
                    {
                        Zipcode newZip = new Zipcode();
                        newZip.ZipcodeValue = proMyDetails.postalcode;
                        City cityId = _helperlandContext.Cities.Where(x => x.CityName == proMyDetails.city).FirstOrDefault();
                        newZip.CityId = cityId.Id;
                        _helperlandContext.Zipcodes.Add(newZip);
                        _helperlandContext.SaveChanges();
                    }

                    userAddress.AddressLine1 = proMyDetails.addressline1;
                    userAddress.AddressLine2 = proMyDetails.addressline2;
                    userAddress.City = proMyDetails.city;
                    userAddress.State = proMyDetails.state;
                    userAddress.PostalCode = proMyDetails.postalcode;
                    userAddress.Email = proMyDetails.email;
                    userAddress.Mobile = proMyDetails.mobile;
                    _helperlandContext.UserAddresses.Update(userAddress);
                    _helperlandContext.SaveChanges();
                    //return Json("Update");
                }
                return Json("updateSuccessfully");
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
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
        public IActionResult NewService()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "New Request";
                ViewBag.IsloggedIn = "success";
                ViewBag.UType = 2;
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }


        [HttpGet]
        public IActionResult GetNewServiceData()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                List<ProviderDashboard> NewService = new List<ProviderDashboard>();
                User user1 = _helperlandContext.Users.Where(x => x.UserId == logedUserid).FirstOrDefault();
                var Alldata = _helperlandContext.ServiceRequests.Where(x => x.ZipCode == user1.ZipCode && x.Status == null && x.ServiceProviderId  == null).ToList();
                foreach (var data in Alldata)
                {
                    var isBlocked = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == logedUserid && x.TargetUserId == data.UserId && x.IsBlocked == true).FirstOrDefault();
                    if (isBlocked == null)
                    {
                        ProviderDashboard NewServiceData = new ProviderDashboard();
                        NewServiceData.ServiceId = data.ServiceRequestId;
                        NewServiceData.ServiceDate = data.ServiceStartDate.ToString("dd/MM/yyyy");
                        NewServiceData.ServiceStartTime = data.ServiceStartDate.ToString("HH:mm");
                        NewServiceData.ServiceEndTime = data.ServiceStartDate.AddHours((double)data.SubTotal).ToString("HH:mm");
                        NewServiceData.Payment = data.TotalCost;
                        NewServiceData.hasPet = data.HasPets;

                        User user = _helperlandContext.Users.Where(x => x.UserId == data.UserId).FirstOrDefault();
                        NewServiceData.CustName = user.FirstName + " " + user.LastName;

                        ServiceRequestAddress serviceRequestAddress = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == data.ServiceRequestId).FirstOrDefault();
                        NewServiceData.add1 = serviceRequestAddress.AddressLine1;
                        NewServiceData.add2 = serviceRequestAddress.AddressLine2;
                        NewServiceData.city = serviceRequestAddress.City;
                        NewServiceData.pincode = serviceRequestAddress.PostalCode;

                        var AllReq = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == logedUserid && x.Status == null).ToList();

                        if (AllReq.Count() > 0)
                        {
                            foreach (var req in AllReq)
                            {
                                var NewServiceStartDate = data.ServiceStartDate;
                                var NewServiceEndDateTime = data.ServiceStartDate.AddHours((double)data.SubTotal);
                                var UpcomingStartDate = req.ServiceStartDate;
                                var UpcomingEndDateTime = req.ServiceStartDate.AddHours((double)req.SubTotal + 1);

                                if (NewServiceStartDate >= UpcomingStartDate && NewServiceStartDate <= UpcomingEndDateTime)
                                {
                                    NewServiceData.ConflictServiceId = req.ServiceRequestId;
                                    break;
                                }
                                else if (NewServiceEndDateTime >= UpcomingStartDate && NewServiceEndDateTime <= UpcomingEndDateTime)
                                {
                                    NewServiceData.ConflictServiceId = req.ServiceRequestId;
                                    break;
                                }
                                else if (NewServiceStartDate < UpcomingStartDate && NewServiceEndDateTime > UpcomingEndDateTime)
                                {
                                    NewServiceData.ConflictServiceId = req.ServiceRequestId;
                                    break;
                                }
                                else
                                {
                                    NewServiceData.ConflictServiceId = null;
                                }
                            }
                        }
                        else
                        {
                            NewServiceData.ConflictServiceId = null;
                        }

                        NewService.Add(NewServiceData);
                    }
                }
                return new JsonResult(NewService);
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpPost]
        public IActionResult AcceptServiceRequest(string acceptSerId)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            ServiceRequest acceptReq = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == Convert.ToInt32(acceptSerId)).FirstOrDefault();
            if(acceptReq.ServiceProviderId == null)
            {
            acceptReq.ServiceProviderId = logedUserid;
            acceptReq.ModifiedBy = logedUserid;
            acceptReq.ModifiedDate = DateTime.Now;
            _helperlandContext.ServiceRequests.Update(acceptReq);
            _helperlandContext.SaveChanges();


                string subject = "A new service booking request has arrived in your area .";
                string mailTitle = "Helperland Service";
                string fromEmail = "";
                string fromEmailPassword = "";

                var AvailableProvider = _helperlandContext.Users.Where(x => x.ZipCode == acceptReq.ZipCode && x.UserId!=logedUserid).ToList();
                if (AvailableProvider != null)
                {
                    foreach (var availPro in AvailableProvider)
                    {
                        string MailBody = "<!DOCTYPE html>" +
                                 "<html> " +
                                 "<body style=\"background -color:#ff7f26;text-align:center;\"> " +
                                 "<h1 style=\"color:#051a80;\">Welcome to Helperland.</h1> " +
                                  "<p>Dear " + availPro.FirstName + " " + availPro.LastName + " ,</p>" +
                                  "<p>Service request with reference id " + acceptReq.ServiceRequestId + " is no more available now.</p>" +
                                  "<p>For more information please Login to your account</p>" +
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


                return Json("Successfully");
            }
            else
            {
                return Json("alreadyBooked");
            }
        }

        public IActionResult UpcomingService()
        {
            
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "UpcomingRequest";
                ViewBag.UType = 2;
                ViewBag.IsloggedIn = "success";
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpGet]
        public IActionResult GetUpcomingServiceData()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                List<ProviderDashboard> Upcoming = new List<ProviderDashboard>();
                var Alldata = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == logedUserid && x.Status == null).ToList();
                foreach (var data in Alldata)
                {
                    ProviderDashboard UpcomingData = new ProviderDashboard();
                    UpcomingData.ServiceId = data.ServiceRequestId;
                    UpcomingData.ServiceDate = data.ServiceStartDate.ToString("dd/MM/yyyy");
                    UpcomingData.ServiceStartTime = data.ServiceStartDate.ToString("HH:mm");
                    UpcomingData.ServiceEndTime = data.ServiceStartDate.AddHours((double)data.SubTotal).ToString("HH:mm");
                    UpcomingData.Payment = data.TotalCost;

                    User user = _helperlandContext.Users.Where(x => x.UserId == data.UserId).FirstOrDefault();
                    UpcomingData.CustName = user.FirstName + " " + user.LastName;

                    ServiceRequestAddress serviceRequestAddress = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == data.ServiceRequestId).FirstOrDefault();
                    UpcomingData.add1 = serviceRequestAddress.AddressLine1;
                    UpcomingData.add2 = serviceRequestAddress.AddressLine2;
                    UpcomingData.city = serviceRequestAddress.City;
                    UpcomingData.pincode = serviceRequestAddress.PostalCode;


                    Upcoming.Add(UpcomingData);
                }
                return new JsonResult(Upcoming);
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpPost]
        public ActionResult CancleRequest(string InputCancleServiceId, string canclecomments)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            ServiceRequest cancleReq = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == Convert.ToInt32(InputCancleServiceId) && x.ServiceProviderId == logedUserid).FirstOrDefault();
            if (cancleReq.ServiceStartDate <= DateTime.Now)
            {
                cancleReq.Status = 1;
            }
            else
            {
                cancleReq.Status = null;
                cancleReq.ServiceProviderId = null;
            }
            cancleReq.Comments = canclecomments;
            cancleReq.ModifiedBy = logedUserid;
            cancleReq.ModifiedDate = DateTime.Now;
            _helperlandContext.ServiceRequests.Update(cancleReq);
            _helperlandContext.SaveChanges();
            return Json("Successfully");
        }

        [HttpPost]
        public ActionResult CompleteRequest(string CompleteReqServiceId)
        {
            int? logedUserid = HttpContext.Session.GetInt32("userid");
            ServiceRequest completeReq = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == Convert.ToInt32(CompleteReqServiceId) && x.ServiceProviderId == logedUserid).FirstOrDefault();
            completeReq.Status = 2;
            completeReq.ModifiedBy = logedUserid;
            completeReq.ModifiedDate = DateTime.Now;
            _helperlandContext.ServiceRequests.Update(completeReq);
            _helperlandContext.SaveChanges();
            return Json("Successfully");
        }

        public IActionResult ServiceHistory()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "Service History";
                ViewBag.IsloggedIn = "success";
                ViewBag.UType = 2;
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
        [HttpGet]
        public IActionResult GetServiceHistoryDetails()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                List<ProviderDashboard> serviceHistory = new List<ProviderDashboard>();
                var Alldata = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == logedUserid && x.Status == 2).ToList();
                foreach (var data in Alldata)
                {
                    ProviderDashboard ServiceHistoryData = new ProviderDashboard();
                    ServiceHistoryData.ServiceId = data.ServiceRequestId;
                    ServiceHistoryData.ServiceDate = data.ServiceStartDate.ToString("dd/MM/yyyy");
                    ServiceHistoryData.ServiceStartTime = data.ServiceStartDate.ToString("HH:mm");
                    ServiceHistoryData.ServiceEndTime = data.ServiceStartDate.AddHours((double)data.SubTotal).ToString("HH:mm");

                    User user = _helperlandContext.Users.Where(x => x.UserId == data.UserId).FirstOrDefault();
                    ServiceHistoryData.CustName = user.FirstName + " " + user.LastName;
                    
                    ServiceRequestAddress serviceRequestAddress = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == data.ServiceRequestId).FirstOrDefault();
                    ServiceHistoryData.add1 = serviceRequestAddress.AddressLine1;
                    ServiceHistoryData.add2 = serviceRequestAddress.AddressLine2;
                    ServiceHistoryData.city = serviceRequestAddress.City;
                    ServiceHistoryData.pincode = serviceRequestAddress.PostalCode;


                    serviceHistory.Add(ServiceHistoryData);
                }
                return new JsonResult(serviceHistory);
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpGet]
        public ActionResult GetServiceSummaryData(int ReqServiceId)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                ServiceRequest reqService = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == ReqServiceId).FirstOrDefault();
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
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }
        public IActionResult MyRating()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "My Rating";
                ViewBag.IsloggedIn = "success";
                ViewBag.UType = 2;
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpGet]
        public ActionResult GetMyRatingData()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                List<ProviderDashboard> MyRating = new List<ProviderDashboard>();
                var RatingData = _helperlandContext.Ratings.Where(x=>x.RatingTo == logedUserid).ToList();
                foreach (var data in RatingData)
                {
                    ProviderDashboard ratingTbldata = new ProviderDashboard();
                    ratingTbldata.ServiceId = data.ServiceRequestId;

                    ServiceRequest serviceRequest = _helperlandContext.ServiceRequests.Where(x => x.ServiceRequestId == data.ServiceRequestId).FirstOrDefault();
                    ratingTbldata.ServiceDate = serviceRequest.ServiceStartDate.ToString("dd/MM/yyyy");
                    ratingTbldata.ServiceStartTime = serviceRequest.ServiceStartDate.ToString("HH:mm");
                    ratingTbldata.ServiceEndTime = serviceRequest.ServiceStartDate.AddHours((double)serviceRequest.SubTotal).ToString("HH:mm");
                    if (data.Comments != null)
                    {
                    ratingTbldata.comment = data.Comments;
                    }
                    else
                    {
                        ratingTbldata.comment = "";
                    }

                    User user = _helperlandContext.Users.Where(x => x.UserId == data.RatingFrom).FirstOrDefault();
                    ratingTbldata.CustName = user.FirstName + " " + user.LastName;

                    Rating rating = _helperlandContext.Ratings.Where(x => x.ServiceRequestId == data.ServiceRequestId).FirstOrDefault();
                    ratingTbldata.SpRatings = rating.Ratings;



                    MyRating.Add(ratingTbldata);
                }
                return new JsonResult(MyRating);
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        public IActionResult BlockCustomer()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                ViewBag.Title = "Block Customer";
                ViewBag.IsloggedIn = "success";
                ViewBag.UType = 2;
                var userid = HttpContext.Session.GetInt32("userid");
                User loggeduser = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
                ViewBag.UserName = loggeduser.FirstName;
                return View();
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

        [HttpGet]

        public ActionResult GetBlockCustData()
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");

                List<BlockCust> blockCust = new List<BlockCust>();
                
                 List<int> AllReq = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == logedUserid && x.Status == 2).Select(x => x.UserId).Distinct().ToList();
                if (AllReq.Count() > 0)
                {
                    foreach (var req in AllReq)
                    {
                        BlockCust blockCustData = new BlockCust();
                        blockCustData.userId = req;

                        var userData = _helperlandContext.Users.Where(x => x.UserId == req).FirstOrDefault();
                        blockCustData.Name = userData.FirstName + " " + userData.LastName;

                        var isBlocked = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == logedUserid && x.TargetUserId == req).FirstOrDefault();

                        if (isBlocked != null)
                        {
                            if (isBlocked.IsBlocked == true)
                            {
                                blockCustData.IsBlocked = true;
                            }
                            else
                            {
                                blockCustData.IsBlocked = false;
                            }
                        }
                        else
                        {
                            blockCustData.IsBlocked = false;
                        }
                        blockCust.Add(blockCustData);
                    }
                    return new JsonResult(blockCust);
                }
                else
                {
                    return Json("noData");
                }
            }
        
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }


        [HttpPost]
        public IActionResult PostBlockCustomer(int CustId)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                if (_helperlandContext.FavoriteAndBlockeds.Any(x => x.UserId == logedUserid && x.TargetUserId == CustId))
                {
                    FavoriteAndBlocked req = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == logedUserid && x.TargetUserId == CustId).FirstOrDefault();
                    req.IsBlocked = true;

                    _helperlandContext.FavoriteAndBlockeds.Update(req);
                    _helperlandContext.SaveChanges();
                }
                else
                {
                    FavoriteAndBlocked req = new FavoriteAndBlocked();
                    req.UserId = Convert.ToInt32(logedUserid);
                    req.TargetUserId = CustId;
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
        public IActionResult UnBlockCustomer(int CustId)
        {
            if (HttpContext.Session.GetInt32("usertypeid") == 2 && HttpContext.Session.GetInt32("userid") != null)
            {
                int? logedUserid = HttpContext.Session.GetInt32("userid");
                FavoriteAndBlocked req = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == logedUserid && x.TargetUserId == CustId).FirstOrDefault();
                req.IsBlocked = false;
                _helperlandContext.FavoriteAndBlockeds.Update(req);
                _helperlandContext.SaveChanges();
                return Json("success");
            }
            return Redirect((Url.Action("Index", "Helperland") + "?loginModal=true"));
        }

    }
}
