namespace TuHu_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Import_Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Import_Bill()
        {
            Import_Bill_Details = new HashSet<Import_Bill_Details>();
        }

        [Key]
        public int Id_Import_Bill { get; set; }

        public int? Id_Staff { get; set; }

        public int? Id_Provider { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_And_Time { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Import_Bill_Details> Import_Bill_Details { get; set; }

        public virtual Provider Provider { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
