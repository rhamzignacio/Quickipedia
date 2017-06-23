using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quickipedia.Models
{
    public class ProfileManagementModel
    {
        public Guid ID { get; set; }
        public string ProfileType { get; set; }
        public string OtherProfileType { get; set; }
        public string SpecialInstruction { get; set; }
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
        public string BookingType { get; set; }
        public string OtherBookingType { get; set; }
    }

    public class ProfileTemplateLinkModel
    {
        public Guid ID { get; set; }
        public string Link { get; set; }
        public string ClientCode { get; set; }
        public string Status { get; set; }
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
    }

    public class ProfileTemplateAttachmentModel
    {
        public Guid ID { get; set; }
        public string Path { get; set; }
        public string ClientCode { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string FileSize { get; set; }
        public string Extension { get; set; }
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