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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.BodyMeasure = new HashSet<BodyMeasure>();
            this.Customer = new HashSet<Customer>();
            this.NutrationProgram = new HashSet<NutrationProgram>();
            this.SportProgram = new HashSet<SportProgram>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> ImageID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string UserIP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BodyMeasure> BodyMeasure { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual Image Image { get; set; }
        public virtual Image Image1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NutrationProgram> NutrationProgram { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SportProgram> SportProgram { get; set; }
    }
}
