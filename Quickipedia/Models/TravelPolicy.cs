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
    
    public partial class TravelPolicy
    {
        public System.Guid ID { get; set; }
        public string LLF { get; set; }
        public string ClassOfService { get; set; }
        public string FlightWindow { get; set; }
        public string AdvancePurchase { get; set; }
        public string LCCCondition { get; set; }
        public string SeatSelection { get; set; }
        public string BaggageAllowance { get; set; }
        public string GroupBookingPolicy { get; set; }
        public string CompanionHCPPersonalTravel { get; set; }
        public string TravelInsurance { get; set; }
        public string ClientCode { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
