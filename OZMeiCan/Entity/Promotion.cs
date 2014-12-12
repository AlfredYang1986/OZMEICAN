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
    
    public partial class Promotion
    {
        public Promotion()
        {
            this.DeliverOrderRows = new HashSet<DeliverOrderRow>();
            this.DeliveryOrders = new HashSet<DeliveryOrder>();
        }
    
        public int Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public Nullable<int> Percentage { get; set; }
        public Nullable<int> Amount { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
    
        public virtual ICollection<DeliverOrderRow> DeliverOrderRows { get; set; }
        public virtual ICollection<DeliveryOrder> DeliveryOrders { get; set; }
    }
}
