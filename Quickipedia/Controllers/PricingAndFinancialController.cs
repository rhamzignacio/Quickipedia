using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Services;
using Quickipedia.Models;

namespace Quickipedia.Controllers
{
    public class PricingAndFinancialController : Controller
    {
        #region Views
        public ActionResult TableOfFeesCategory()
        {
            return View();
        }

        public ActionResult PricingModel()
        {
            return View();
        }

        public ActionResult FormOfPayment()
        {
            return View();
        }

        public ActionResult BillingCollectionFinance()
        {
            return View();
        }

        public ActionResult RefundProcess()
        {
            return View();
        }

        public ActionResult ScheduleOfInvoice()
        {
            return View();
        }

        public ActionResult TableOfFees()
        {
            return View();
        }

        public ActionResult InvoiceAttachment()
        {
            return View();
        }

        public ActionResult Others()
        {
            return View();
        }

        public ActionResult FareReference()
        {
            return View();
        }

        #endregion

        #region Functions
        //==============FARE COMPARISON==============
        [HttpPost]
        public JsonResult GetFareRef()
        {
            string serverResponse = "";

            var fare = PricingAndFinancialService.GetFareComparison(out serverResponse);

            return Json(new { errorMessage = serverResponse, fare = fare });
        }

        [HttpPost]
        public JsonResult SaveFareRef(FareComparisonModel fare)
        {
            string serverResponse = "";

            if (fare != null)
                PricingAndFinancialService.SaveFareComparison(fare, out serverResponse);

            return Json(serverResponse);
        }

        //=================OTHERS====================
        [HttpPost]
        public JsonResult GetOthers()
        {
            string serverResponse = "";

            var other = PricingAndFinancialService.GetOther(out serverResponse);

            return Json(other);
        }

        [HttpPost]
        public JsonResult SaveOthers(OtherPricingAndFinancialModel other)
        {
            string serverResponse = "";

            if(other != null)
            {
                PricingAndFinancialService.SaveOther(other, out serverResponse);
            }

            return Json(serverResponse);
        }

        //=================TABLE OF FEES CATEGORY===============
        [HttpPost]
        public JsonResult GetTableOfFeesCategory()
        {
            string serverResponse = "";

            var category = PricingAndFinancialService.GetTableOfFeesCategory(out serverResponse);

            return Json(new { category = category, errorMessage = serverResponse });
        }

        [HttpPost]
        public JsonResult SaveCategory(TableOfFeesCategoryModel category)
        {
            string serverResponse = "";

            PricingAndFinancialService.SaveTableOfFeesCategory(category, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult DeleteCategory(TableOfFeesCategoryModel category)
        {
            string serverResponse = "";

            if (category != null)
            {
                category.Status = "X";

                PricingAndFinancialService.SaveTableOfFeesCategory(category, out serverResponse);
            }

            return Json(serverResponse);
        }

        //=================TABLE OF FEES====================
        [HttpPost]
        public JsonResult GetCategoryDropDown()
        {
            string serverReponse = "";

            var dropdown = PricingAndFinancialService.GetCategoryDropDown(out serverReponse);

            var category = PricingAndFinancialService.GetTableOfFeesCategory(out serverReponse);

            return Json(new { dropdown = dropdown, category = category,
                errorMessage = serverReponse });
        }

        [HttpPost]
        public JsonResult GetInvoiceAttachment()
        {
            string serverResponse = "";

            var inv = PricingAndFinancialService.GetInvoiceAttachment();

            return Json(inv);
        }

        [HttpPost]
        public JsonResult SaveInvoiceAttachment(InvoiceAttachmentModel invoice)
        {
            string serverResponse = "";

            PricingAndFinancialService.SaveInvoiceAttachment(invoice, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult SaveTableOfFees(TableOfFeesModels fees)
        {
            string serverResponse = "";

            PricingAndFinancialService.SaveTableOfFees(fees, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetTableOfFees()
        {
            string serverResponse = "";

            var fees = PricingAndFinancialService.GetTableOfFees(out serverResponse);

            return Json(fees);
        }

        //=================REFUND PROCESS===================
        [HttpPost]
        public JsonResult GetScheduleOfInvoice()
        {
            string serverResponse = "";

            var schedules = PricingAndFinancialService.GetScheduleOfInvoice(out serverResponse);

            return Json(schedules);
        }

        [HttpPost]
        public JsonResult SaveScheduleOfInvoice(ScheduleOfInvoiceModel schedule)
        {
            string serverResponse = "";

            if (schedule != null)
                PricingAndFinancialService.SaveScheduleOfInvoice(schedule, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult GetRefundProcess()
        {
            return Json(PricingAndFinancialService.GetRefundProcess(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateRefundProcess(RefundProcessModel refundProcess)
        {
            string serverResponse = "";

            if (refundProcess != null)
                PricingAndFinancialService.SaveRefundProcess(refundProcess, out serverResponse);

            return Json(serverResponse);
        }

        //==================BILLING COLLECTION FINANCE================
        [HttpPost]
        public JsonResult GetBillingCollectionFinance()
        {
            return Json(PricingAndFinancialService.GetBillingCollectionFinance(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateBillingCollectionFinance(BillingCollectionFinanceModel billingCollections)
        {
            string serverResponse = "";

            if(billingCollections != null)
                PricingAndFinancialService.SaveBillingCollection(billingCollections, out serverResponse);

            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult DeleteBillingCollectionFinance(BillingCollectionFinanceModel billing)
        {
            string serverResponse = "";

            if(billing != null)
            {
                billing.Status = "X";

                PricingAndFinancialService.SaveBillingCollection(billing, out serverResponse);
            }

            return Json(serverResponse);
        }

        //===============FORM OF PAYMENT===================
        [HttpPost]
        public JsonResult GetFormOfPayment()
        {
            return Json(PricingAndFinancialService.GetFormOfPayment(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdateFormOfPayment(FormOfPaymentModel fop)
        {
            string serverResponse = "";

            if (fop != null)
                PricingAndFinancialService.SaveFormOfPayment(fop, out serverResponse);
            
            return Json(serverResponse);
        }

        //=============PRICING MODEL=================
        [HttpPost]
        public JsonResult GetPricingModel()
        {
            return Json(PricingAndFinancialService.GetPricingModel(UniversalHelpers.SelectedClient));
        }

        [HttpPost]
        public JsonResult AddUpdatePricingModel(PricingModels pricingModel, string type)
        {
            string serverResponse = "";

            if(pricingModel != null)
                PricingAndFinancialService.SavePricingModel(pricingModel, out serverResponse);

            return Json(serverResponse);
        }
       
        #endregion
    }
}