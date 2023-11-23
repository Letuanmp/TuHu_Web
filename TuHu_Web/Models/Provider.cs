namespace TuHu_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Provider")]
    public partial class Provider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Provider()
        {
            Import_Bill = new HashSet<Import_Bill>();
        }

        [Key]
        public int Id_Provider { get; set; }

        [StringLength(50)]
        public string Name_Provider { get; set; }

        [StringLength(50)]
        public string Address_Provider { get; set; }

        public int? Phone_Number_Provider { get; set; }

        [StringLength(50)]
        public string Email_Provider { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Import_Bill> Import_Bill { get; set; }
    }
}
