namespace TuHu_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bill_Of_Sale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill_Of_Sale()
        {
            Sales_Bill_Details = new HashSet<Sales_Bill_Details>();
        }

        [Key]
        public int Id_Bill_Of_Sale { get; set; }

        public int? Id_Staff { get; set; }

        public int? Id_Customer { get; set; }

        public DateTime? Date_And_Time { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Staff Staff { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sales_Bill_Details> Sales_Bill_Details { get; set; }
    }
}
