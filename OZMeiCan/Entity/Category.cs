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
    
    public partial class Category
    {
        public Category()
        {
            this.DishCategory = new HashSet<DishCategory>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentCategoryId { get; set; }
    
        public virtual ICollection<DishCategory> DishCategory { get; set; }
    }
}
