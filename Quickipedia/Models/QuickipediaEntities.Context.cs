﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuickipediaEntities : DbContext
    {
        public QuickipediaEntities()
            : base("name=QuickipediaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Advisory> Advisory { get; set; }
        public virtual DbSet<Airlines> Airlines { get; set; }
        public virtual DbSet<AncillariesFees> AncillariesFees { get; set; }
        public virtual DbSet<BillingCollectionFinanceManager> BillingCollectionFinanceManager { get; set; }
        public virtual DbSet<CarCorporateCode> CarCorporateCode { get; set; }
        public virtual DbSet<CarPolicy> CarPolicy { get; set; }
        public virtual DbSet<CarProgramAttachment> CarProgramAttachment { get; set; }
        public virtual DbSet<CarProgramLink> CarProgramLink { get; set; }
        public virtual DbSet<ClientBookerApprover> ClientBookerApprover { get; set; }
        public virtual DbSet<ClientContactPerson> ClientContactPerson { get; set; }
        public virtual DbSet<ClientDomesticTC> ClientDomesticTC { get; set; }
        public virtual DbSet<ClientInternationalTC> ClientInternationalTC { get; set; }
        public virtual DbSet<ClientProfile> ClientProfile { get; set; }
        public virtual DbSet<DisallowedAirlines> DisallowedAirlines { get; set; }
        public virtual DbSet<DomesticBookingProcess> DomesticBookingProcess { get; set; }
        public virtual DbSet<FormOfPayment> FormOfPayment { get; set; }
        public virtual DbSet<HotelCorporateCode> HotelCorporateCode { get; set; }
        public virtual DbSet<HotelPolicy> HotelPolicy { get; set; }
        public virtual DbSet<HotelProgramAttachement> HotelProgramAttachement { get; set; }
        public virtual DbSet<HotelProgramLink> HotelProgramLink { get; set; }
        public virtual DbSet<InternationalBookingProcess> InternationalBookingProcess { get; set; }
        public virtual DbSet<InvoiceAttachment> InvoiceAttachment { get; set; }
        public virtual DbSet<MICEGroupBookingPolicy> MICEGroupBookingPolicy { get; set; }
        public virtual DbSet<MICEPolicy> MICEPolicy { get; set; }
        public virtual DbSet<MICEPricing> MICEPricing { get; set; }
        public virtual DbSet<MICETicketingApproval> MICETicketingApproval { get; set; }
        public virtual DbSet<OtherAir> OtherAir { get; set; }
        public virtual DbSet<OtherPricingAndFinancial> OtherPricingAndFinancial { get; set; }
        public virtual DbSet<PreferredAirlines> PreferredAirlines { get; set; }
        public virtual DbSet<PricingModel> PricingModel { get; set; }
        public virtual DbSet<ProfileManagement> ProfileManagement { get; set; }
        public virtual DbSet<RailCorporateCode> RailCorporateCode { get; set; }
        public virtual DbSet<RailPolicy> RailPolicy { get; set; }
        public virtual DbSet<RailProgramAttachment> RailProgramAttachment { get; set; }
        public virtual DbSet<RailProgramLink> RailProgramLink { get; set; }
        public virtual DbSet<RefundProcess> RefundProcess { get; set; }
        public virtual DbSet<ScheduleOfInvoiceReceiving> ScheduleOfInvoiceReceiving { get; set; }
        public virtual DbSet<SLA> SLA { get; set; }
        public virtual DbSet<SMMPForMSD> SMMPForMSD { get; set; }
        public virtual DbSet<TableOfFees> TableOfFees { get; set; }
        public virtual DbSet<TravelPolicy> TravelPolicy { get; set; }
        public virtual DbSet<TravelSecurity> TravelSecurity { get; set; }
        public virtual DbSet<TripAuthorizerProcess> TripAuthorizerProcess { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }
        public virtual DbSet<UserClient> UserClient { get; set; }
        public virtual DbSet<VIP> VIP { get; set; }
        public virtual DbSet<VisaAndDocumentation> VisaAndDocumentation { get; set; }
    }
}
