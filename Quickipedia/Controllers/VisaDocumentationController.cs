using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Services;
using Quickipedia.Models;

namespace Quickipedia.Controllers
{
    public class VisaDocumentationController : Controller
    {
        #region Views
        public ActionResult VisaDocumentation()
        {
            return View();
        }
        #endregion

        #region Functions
        [HttpPost]
        public JsonResult GetVisaDocumentation()
        {
            return Json(VisaDocumentationService.GetVisaDocumentation(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateVisa(VisaDocumentationModel visaDocumentation)
        {
            string serverResponse = "";

            if (visaDocumentation != null)
                VisaDocumentationService.SaveVisaDocumentation(visaDocumentation, out serverResponse);

            return Json(serverResponse);
        }
        #endregion
    }
}