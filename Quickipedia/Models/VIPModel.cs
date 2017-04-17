using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quickipedia.Models
{
    public class VIPModel
    {
        public Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string VIPList { get; set; }
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
        public string Name { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
    }
}