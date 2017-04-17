using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quickipedia.Models
{
    public class AirModel { }

    public class OtherAirModel
    {
        public Guid ID { get; set; }
        public string Value { get; set; }
        public string ClientCode { get; set; }
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

    public class SLAModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string ResponseTime { get; set; }
        public int? NoOfQuotationOptions { get; set; }
        public string QuotationFormat { get; set; }
        public string QuotationFormat_Other { get; set; }
        public string AuthorityToTicket { get; set; }
        public string FareAudit { get; set; }
        public string AOHReminder { get; set; }
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
        public string Remarks { get; set; }
    }

    public class TripAuthorizerProcessModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string TripAuthorizer { get; set; }
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

    public class TravelPolicyModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string LLF { get; set; }
        public string ClassOfService { get; set; }
        public string FlightWindow { get; set; }
        public string AdvancePurchase { get; set; }
        public string LCCCondition { get; set; }
        public string SeatSelection { get; set; }
        public string BaggageAllowance { get; set; }
        public string GroupBookingPolicy { get; set; }
        public string CompanionHCPersonalTravel { get; set; }
        public string TravelInsurance { get; set; }
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

    public class PreferredAirlineModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public string AirlineNo { get; set; }
        public string Status { get; set; }
        public string CorporateCodes { get; set; }
        public DateTime? ContractStart { get; set; }
        public string ShowContractStart
        {
            get
            {
                if (ContractStart != null)
                    return ContractStart.ToString();
                else
                    return "";
            }
        }
        public DateTime? ContractEnd { get; set; }
        public string ShowContractEnd
        {
            get
            {
                if (ContractEnd != null)
                    return ContractEnd.ToString();
                else
                    return "";
            }
        }
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

    public class TravelSecurityModel
    {
        public Guid ID { get; set; }
        public string TravelSecurity { get; set; }
        public string ClientCode { get; set; }
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