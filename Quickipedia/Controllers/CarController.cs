using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Services;
using Quickipedia.Models;
using System.IO;

namespace Quickipedia.Controllers
{
    public class CarController : Controller
    {
        #region Views
        public ActionResult CarPolicy()
        {
            return View();
        }

        public ActionResult CarCorporateCode()
        {
            return View();
        }

        public ActionResult Program()
        {
            return View();
        }
        #endregion

        #region Functions
        [HttpPost]
        public JsonResult FileUpload()
        {
            var httpRequest = System.Web.HttpContext.Current.Request;
            HttpFileCollection uploadFiles = httpRequest.Files;
            var docfiles = new List<string>();
            string newFileName = "";
            string ext = "";

            if(httpRequest.Files.Count > 0)
            {
                for(int i = 0; i < uploadFiles.Count; i++)
                {
                    HttpPostedFile postedFile = uploadFiles[i];

                    ext = Path.GetExtension(postedFile.FileName);

                    newFileName = DateTime.Now.ToString("MMddyyyHHmmss");

                    var filePath = Server.MapPath(@"\FileUploads\" + newFileName + ext);

                    postedFile.SaveAs(filePath);

                    using (var db = new QuickipediaEntities())
                    {
                        CarProgramAttachment newAttachment = new CarProgramAttachment
                        {
                            ID = Guid.NewGuid(),
                            ClientCode = UniversalHelpers.SelectedClient,
                            FileName = postedFile.FileName,
                            FileSize = (postedFile.ContentLength / 1024).ToString(),
                            Extension = ext,
                            Path = newFileName + ext,
                            ModifiedBy = UniversalHelpers.CurrentUser.ID,
                            ModifiedDate = DateTime.Now
                        };

                        db.Entry(newAttachment).State = System.Data.Entity.EntityState.Added;

                        db.SaveChanges();
                    }
                }
            }

            return Json("");
        }

        [HttpPost]
        public JsonResult UpdateAttachment(CarAttachmentModel files)
        {
            string serverResponse = "";

            if(files != null)
            {
                CarService.UpdateAttachments(files, out serverResponse);
            }

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult DeleteAttachment(CarAttachmentModel file)
        {
            string serverResponse = "";

            if(file != null)
            {
                file.Status = "X";

                CarService.UpdateAttachments(file, out serverResponse);
            }

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetCarProgram()
        {
            string serverResponse = "";

            var links = CarService.GetLinks(out serverResponse);

            var attachments = CarService.GetAttachments(out serverResponse);

            return Json(new { links = links , attachments = attachments});
        }

        [HttpPost]
        public JsonResult DeleteHotelProgram(CarLinksModel links)
        {
            string serverResponse = "";

            links.Status = "X";

            if (links != null)
                CarService.SaveCarLinks(links, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult SaveCarProgram(CarLinksModel links)
        {
            string serverResponse = "";

            if (links != null)
                CarService.SaveCarLinks(links, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetCarCorporateCode()
        {
            return Json(CarService.GetCarCorporateCode(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateCarCorporateCode(CarCorporateCodeModel carCorporateCode)
        {
            string serverResponse = "";

            if (carCorporateCode != null)
                CarService.SaveCarCorporateCode(carCorporateCode, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetCarPolicy()
        {
            return Json(CarService.GetCarPolicy(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateCarPolicy(CarPolicyModel carPolicy)
        {
            string serverResponse = "";

            if (carPolicy != null)
                CarService.SaveCarPolicy(carPolicy, out serverResponse);

            return Json(serverResponse);
        }
        #endregion
    }
}