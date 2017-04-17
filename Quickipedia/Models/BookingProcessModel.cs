using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quickipedia.Models
{
    public class BookingProcessModel
    {
    }

    public class INTLBookingProcessModel
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

    public class DOMBookingProcessModel
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