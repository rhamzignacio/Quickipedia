using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quickipedia.Models
{
    public class AncillariesModel
    {
        public Guid ID { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal? PHPAmount { get; set; }
        public decimal? USDAmount { get; set; }
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
        public string Status { get; set; }
    }
}