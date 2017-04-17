using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Services;
using Quickipedia.Models;

namespace Quickipedia.Controllers
{
    public class ClientBookerApproverController : Controller
    {
        // GET: ClientBookerApprover
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddUpdateClientBooker(ClientBookerApproverModel booker)
        {
            string serverResponse = "";

            if (booker != null)
                ClientBookerApproverService.SaveClientBooker(booker, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetClientBooker()
        {
            return Json(ClientBookerApproverService.GetClientBooker());
        }
    }
}