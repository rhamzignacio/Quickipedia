using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Models;
using Quickipedia.Services;

namespace Quickipedia.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult NewClient()
        {
            return View();
        }

        public ActionResult UnderConstruction()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetClientDropDown()
        {
            return Json(UserService.GetClientDropDown());
        }

        [HttpPost]
        public JsonResult Login(string username, string password)
        {
            string serverResponse = "";

            UserModel user = UserService.ValidateUserLogin(username, password, out serverResponse);

            if (user != null)
            {
                AccountService.LoginToSession(user);
            }

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult Logout()
        {
            string serverResponse = "";

            AccountService.LogoutFromSession(out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult SelectClient(string clientcode)
        {
            var client = UniversalHelpers.CurrentUser;

            AccountService.SelectedClientToSession(clientcode);

            if (clientcode == "New Client")
            {
                ClientProfile clProf = new ClientProfile
                {
                    ClientName = "New Client"
                };

                return Json(clProf);
            }
            else
                return Json(AccountService.GetClientProfile(clientcode));
        }

        public JsonResult GetCurrentUser()
        {
            var clientProfile = AccountService.GetClientProfile(UniversalHelpers.SelectedClient);

            return Json(new { obj = UniversalHelpers.CurrentUser, clients = UserService.GetClientDropDown(),
                selectedclient = clientProfile });
        }
    }
}