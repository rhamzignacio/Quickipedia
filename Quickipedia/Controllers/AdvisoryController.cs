using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Models;
using Quickipedia.Services;

namespace Quickipedia.Controllers
{
    public class AdvisoryController : Controller
    {
        // GET: Advisory
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAvisory()
        {
            string serverResponse = "";

            var advisory = AdvisoryService.GetAdvisory(out serverResponse);

            return Json(new { advisory = advisory });    
        }

        [HttpPost]
        public JsonResult SaveAdvisory(AdvisoryModel advisory)
        {
            string serverResponse = "";

            AdvisoryService.SaveAdvisory(advisory, out serverResponse);

            return Json(serverResponse);
        }
    }
}