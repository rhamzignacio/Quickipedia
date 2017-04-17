using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quickipedia.Models
{
    public class HotelModel
    {
    }
    public class HotelCorporateCodeModel
    {
        public Guid ID { get; set; }
        public string CorporateCode { get; set; }
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
    
    public class HotelPolicyModel
    {
        public Guid ID { get; set; }
        public string Policy { get; set; }
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

    public class HotelLinksModel
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

    public class HotelAttachmentModel
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