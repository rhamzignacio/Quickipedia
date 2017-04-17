using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Models;
using Quickipedia.Services;

namespace Quickipedia.Controllers
{
    public class ProfileManagementController : Controller
    {
        // GET: ProfileManagement
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetProfileManagement()
        {
            var profile = ProfileManagementService.GetProfileManagement();

            return Json(profile);
        }

        [HttpPost]
        public JsonResult SaveProfileMangement(ProfileManagementModel profile)
        {
            string serverResponse = "";

            if(profile != null)
            {
                ProfileManagementService.SaveProfileManagement(profile, out serverResponse);
            }

            return Json(serverResponse);
        }
    }
}