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
    
    public partial class Geolocation
    {
        public Geolocation()
        {
            this.Restaurant = new HashSet<Restaurant>();
            this.UserProfile = new HashSet<UserProfile>();
        }
    
        public int LocId { get; set; }
        public Nullable<int> Suburb { get; set; }
        public double Altitude { get; set; }
        public double Latitude { get; set; }
    
        public virtual Suburb Suburb1 { get; set; }
        public virtual ICollection<Restaurant> Restaurant { get; set; }
        public virtual ICollection<UserProfile> UserProfile { get; set; }
    }
}
