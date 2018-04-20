using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quickipedia.Models
{
    public class PricingAndFinancialModel { }

    public class EcardAdminFeeModel
    {
        public string ClientCode { get; set; }
        public bool AirFare { get; set; }
        public bool ServiceFee { get; set; }
        public bool Others { get; set; }
        public decimal? Divide { get; set; }
        public decimal? Multiply { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return DateTime.Parse(ModifiedDate.ToString()).ToShortDateString();
                else
                    return "";
            }
        }

        private decimal? _showDivide;
        public decimal? ShowDivide
        {
            get
            {
                if (Divide != null)
                    return Divide * 100;
                else
                    return null;
            }
            set { _showDivide = value; }
        }

        private decimal? _showMultiply;
        public decimal? ShowMultiply
        {
            get
            {
                if (Multiply != null)
                    return Multiply * 100;
                else
                    return null;
            }
            set { _showMultiply = value; }
        }
    }

    public class FareComparisonModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string LF { get; set; }
        public string LowFare { get; set; }
        public string RF { get; set; }
        public string ReferenceFare { get; set; }
        public string CarStandardFare { get; set; }
        public string HotelStandardFare { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return DateTime.Parse(ModifiedDate.ToString()).ToShortDateString();
                else
                    return "";
            }
        }
    }

    public class TableOfFeesCategoryDropDown
    {
        public Guid ID { get; set; }
        public string Category { get; set; }
    }

    public class TableOfFeesCategoryModel
    {
        public Guid ID { get; set; }
        public string CategoryName { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return DateTime.Parse(ModifiedDate.ToString()).ToShortDateString();
                else
                    return "";
            }  
        }
        public int? ArrangeBy { get; set; }
        public string Status { get; set; }
    }

    public class OtherPricingAndFinancialModel
    {
        public Guid ID { get; set; }
        public string Value { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return ModifiedDate.ToString();
                else
                    return "";
            }
        }
        public string ClientCode { get; set; }
    }

    public class InvoiceAttachmentModel
    {
        public Guid ID { get; set; }
        public string Value { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedBy { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return ModifiedDate.ToString();
                else
                    return "";
            }
        }
    }

    public class TableOfFeesModels
    {
        private string ConvertNumeric(decimal? val)
        {
            return decimal.Parse(val.ToString()).ToString("#,##0.00");
        }

        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public Guid? CategoryID { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal? PHPTraditionalGDS { get; set; }
        public decimal? PHPNonGDS { get; set; }
        public decimal? PHPMice { get; set; }
        public decimal? PHPOnline { get; set; }
        public decimal? USDTraditionalGDS { get; set; }
        public decimal? USDNonGDS { get; set; }
        public decimal? USDMice { get; set; }
        public decimal? USDOnline { get; set; }

        public string ShowPHPTraditionalGDS
        {
            get
            {
                if (PHPTraditionalGDS != null)
                    return ConvertNumeric(PHPTraditionalGDS);
                else
                    return "";
            }
        }

        public string ShowPHPNonGDS
        {
            get
            {
                if (PHPNonGDS != null)
                    return ConvertNumeric(PHPNonGDS);
                else
                    return "";
            }
        }

        public string ShowPHPMice
        {
            get
            {
                if (PHPMice != null)
                    return ConvertNumeric(PHPMice);
                else
                    return "";
            }
        }

        public string ShowPHPOnline
        {
            get
            {
                if (PHPOnline != null)
                    return ConvertNumeric(PHPOnline);
                else
                    return "";
            }
        }

        public string ShowUSDTraditionalGDS
        {
            get
            {
                if (USDTraditionalGDS != null)
                    return ConvertNumeric(USDTraditionalGDS);
                else
                    return "";
            }
        }

        public string ShowUSDNonGDS
        {
            get
            {
                if (USDNonGDS != null)
                    return ConvertNumeric(USDNonGDS);
                else
                    return "";
            }
        }

        public string ShowUSDMice
        {
            get
            {
                if (USDMice != null)
                    return ConvertNumeric(USDMice);
                else
                    return "";
            }
        }

        public string ShowUSDOnline
        {
            get
            {
                if (USDOnline != null)
                    return ConvertNumeric(USDOnline);
                else
                    return "";
            }
        }

        public string Status { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return ModifiedDate.ToString();
                else
                    return "";
            }
        }
    }

    public class PricingModels
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }

        public bool? BundleFlag { get; set; }
        public bool? UnbundledFlag { get; set; }
        public bool? RetainFlag { get; set; }
        public bool? ReturnFlag { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return ModifiedDate.ToString();
                else
                    return "";
            }
        }
    }

    public class FormOfPaymentModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string FOP { get; set; }
        public string FOP_Others { get; set; }
        public string CCLiquidationProcess { get; set; }
        public string CCLiquidation_Others { get; set; }       
        public string TaxType { get; set; }
        public string ClientName { get; set; }
        public string IssuedCard { get; set; }
        public string IssuedCard_Others { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return ModifiedDate.ToString();
                else
                    return "";
            }
        }
        public string MerchantFee { get; set; }
        public string Currency { get; set; }
        public string OtherCurrency { get; set; }
    }

    public class BillingCollectionFinanceModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Status { get; set; }
        public int IDNo { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return ModifiedDate.ToString();
                else
                    return "";
            }
        }
    }

    public class RefundProcessModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string Process { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return ModifiedDate.ToString();
                else
                    return "";
            }
        }
    }

    public class ScheduleOfInvoiceModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string Value { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ShowModifiedDate
        {
            get
            {
                if (ModifiedDate != null)
                    return ModifiedDate.ToString();
                else
                    return "";
            }
        }
    }
}