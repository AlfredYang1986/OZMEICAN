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
    
    public partial class StateProvince
    {
        public StateProvince()
        {
            this.Suburb = new HashSet<Suburb>();
        }
    
        public int Id { get; set; }
        public string Code { get; set; }
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Suburb> Suburb { get; set; }
    }
}
