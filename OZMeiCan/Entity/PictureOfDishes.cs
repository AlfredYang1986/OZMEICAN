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
    
    public partial class PictureOfDishes
    {
        public PictureOfDishes()
        {
            this.DishPicture = new HashSet<DishPicture>();
        }
    
        public int Id { get; set; }
        public byte[] Thumbnail { get; set; }
        public string Path { get; set; }
    
        public virtual ICollection<DishPicture> DishPicture { get; set; }
    }
}
