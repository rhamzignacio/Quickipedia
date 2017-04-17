using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Models;
using Quickipedia.Services;

namespace Quickipedia.Controllers
{
    public class AncillariesController : Controller
    {
        // GET: Ancillaries
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAncillaries()
        {
            string serverResponse = "";

            var ancillaries = AncillariesService.GetAncillaries(out serverResponse);

            return Json(new { ancillaries = ancillaries });
        }

        [HttpPost]
        public JsonResult SaveAncillaries(AncillariesModel ancillaries)
        {
            string serverResponse = "";

            if(ancillaries != null)
                AncillariesService.SaveAncillaries(ancillaries, out serverResponse);

            return Json(serverResponse);
        }
    }
}