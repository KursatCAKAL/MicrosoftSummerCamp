//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Robolife_Admin_Data_Layer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> NutritionID { get; set; }
        public Nullable<int> SportPID { get; set; }
        public Nullable<int> BodyAnalizeID { get; set; }
        public Nullable<int> CrashAreaID { get; set; }
    
        public virtual CrashArea CrashArea { get; set; }
        public virtual User User { get; set; }
    }
}
