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
    
    public partial class CreditCard
    {
        public CreditCard()
        {
            this.UserProfile = new HashSet<UserProfile>();
        }
    
        public int CreditCardID { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public byte ExpMonth { get; set; }
        public short ExpYear { get; set; }
    
        public virtual ICollection<UserProfile> UserProfile { get; set; }
    }
}
