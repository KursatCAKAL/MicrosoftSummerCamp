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
    
    public partial class ExercisesAct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExercisesAct()
        {
            this.SportProgram = new HashSet<SportProgram>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DescriptionURL { get; set; }
        public Nullable<int> ImageID { get; set; }
        public Nullable<int> TargetRegionID { get; set; }
    
        public virtual CrashArea CrashArea { get; set; }
        public virtual Image Image { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SportProgram> SportProgram { get; set; }
    }
}
