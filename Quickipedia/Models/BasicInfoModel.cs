using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quickipedia.Models
{
    public class BasicInfoModel
    {
        public string ClientName { get; set; }
        public string ClientType { get; set; }
        public string ClientCode { get; set; }
        public string Address { get; set; }
        public List<TravelConsultantModel> InternationalTCs { get; set; }
        public List<TravelConsultantModel> DomesticTCs { get; set; }
        public List<ClientContactPersonModel> ClientContactPersons { get; set; }
        public string Biller { get; set; }
        public string AccountOfficerManager { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string ShowModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? BillerID { get; set; }
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
        public DateTime? ContractStartDate { get; set; }
        public string ShowContractStartDate
        {
            get
            {
                if (ContractStartDate != null)
                    return ContractStartDate.ToString();
                else
                    return "";
            }
        }

        public DateTime? ContractEndDate { get; set; }
        public string ShowContractEndDate
        {
            get
            {
                if (ContractEndDate != null)
                    return ContractEndDate.ToString();
                else
                    return "";
            }
        }
    }

}