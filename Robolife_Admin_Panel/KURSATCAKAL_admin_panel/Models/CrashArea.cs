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
    
    public partial class CrashArea
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CrashArea()
        {
            this.Customer = new HashSet<Customer>();
            this.ExercisesAct = new HashSet<ExercisesAct>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> CrashRate { get; set; }
        public Nullable<int> CustomerID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExercisesAct> ExercisesAct { get; set; }
    }
}
