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
    
    public partial class DeliverOrderRow
    {
        public int OrderId { get; set; }
        public int DishId { get; set; }
        public double SubTotal { get; set; }
        public Nullable<int> PromoId { get; set; }
        public double Distance { get; set; }
    
        public virtual DeliveryOrder DeliveryOrder { get; set; }
        public virtual Dish Dish { get; set; }
        public virtual Promotion Promotion { get; set; }
    }
}
