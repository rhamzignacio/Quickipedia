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
    
    public partial class EcardAdminFee
    {
        public string ClientCode { get; set; }
        public string AirFare { get; set; }
        public string ServiceFee { get; set; }
        public string Others { get; set; }
        public Nullable<decimal> Divide { get; set; }
        public Nullable<decimal> Multiply { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool AirFareFlag { get; set; }
        public bool ServiceFeeFlag { get; set; }
        public bool OtherFeeFlag { get; set; }
        public bool AirFareUSD { get; set; }
        public bool ServiceFeeUSD { get; set; }
        public bool OthersUSD { get; set; }
        public Nullable<decimal> DivideUSD { get; set; }
        public Nullable<decimal> MultiplyUSD { get; set; }
    }
}
