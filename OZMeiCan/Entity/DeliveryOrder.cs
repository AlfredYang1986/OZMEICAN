//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OZMeiCan.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class DeliveryOrder
    {
        public DeliveryOrder()
        {
            this.DeliverOrderRow = new HashSet<DeliverOrderRow>();
            this.Payment = new HashSet<Payment>();
        }
    
        public int OrderId { get; set; }
        public double PriceTotal { get; set; }
        public Nullable<System.DateTime> OrderDateTime { get; set; }
        public string Status { get; set; }
        public Nullable<int> PromoId { get; set; }
        public string OrderRef { get; set; }
        public double DeliveryCost { get; set; }
        public Nullable<int> LocId { get; set; }
        public string ContactNo { get; set; }
    
        public virtual ICollection<DeliverOrderRow> DeliverOrderRow { get; set; }
        public virtual Geolocation Geolocation { get; set; }
        public virtual Promotion Promotion { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
