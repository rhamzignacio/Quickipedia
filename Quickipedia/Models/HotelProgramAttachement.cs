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
    
    public partial class HotelProgramAttachement
    {
        public System.Guid ID { get; set; }
        public string ClientCode { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public string FileSize { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
