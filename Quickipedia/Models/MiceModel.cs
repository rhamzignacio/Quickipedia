using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quickipedia.Models
{
    public class MiceModel
    {
    }

    public class MicePolicyModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string Policy { get; set; }
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

    public class SMMPForMSDModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string SMMPForMSD { get; set; }
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

    public class GroupBookingPolicyModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string GroupPolicy { get; set; }
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

    public class MicePricingModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string Pricing { get; set; }
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

    public class MiceTicketApprovalModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string TicketingApproval { get; set; }
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