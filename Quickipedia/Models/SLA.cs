//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quickipedia.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SLA
    {
        public System.Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string ResponseTime { get; set; }
        public string NoOfQuotationOptions { get; set; }
        public string QuotationFormat { get; set; }
        public string AuthorityToTicket { get; set; }
        public string FareAudit { get; set; }
        public string AOHReminder { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string QuotationFormat_Other { get; set; }
        public string Remarks { get; set; }
    }
}
