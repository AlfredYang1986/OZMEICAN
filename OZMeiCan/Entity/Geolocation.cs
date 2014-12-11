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
        public int SuburbId { get; set; }
        public Nullable<double> Altitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public string Street { get; set; }
        public int StreetNo { get; set; }
        public string UnitNo { get; set; }
    
        public virtual Suburb Suburb { get; set; }
        public virtual ICollection<Restaurant> Restaurant { get; set; }
        public virtual ICollection<UserProfile> UserProfile { get; set; }
    }
}
