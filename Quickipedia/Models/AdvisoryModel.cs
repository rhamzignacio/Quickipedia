using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quickipedia.Models
{
    public class AdvisoryModel
    {
        public Guid ID { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
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

    public class AdvisoryReadModel
    {
        public Guid ID { get; set; }
        public Guid? AdvisoryID { get; set; }
        public Guid? UserID { get; set; }
        public DateTime? ReadDate { get; set; }
        public string ShowReadDate { get; set; }
        public string ShowUser { get; set; }
    }
}