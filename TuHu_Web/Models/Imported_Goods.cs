namespace TuHu_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Imported_Goods
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Imported_Goods()
        {
            Import_Bill_Details = new HashSet<Import_Bill_Details>();
        }

        [Key]
        public int Id_Imported_Goods { get; set; }

        [StringLength(50)]
        public string Name_Imported_Goods { get; set; }

        public double? Number_Imported_Goods { get; set; }

        public double? Price_Imported_Goods { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Expriry_Date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Import_Bill_Details> Import_Bill_Details { get; set; }
    }
}
