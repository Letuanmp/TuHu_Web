namespace TuHu_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Sales_Bill_Details = new HashSet<Sales_Bill_Details>();
        }

        [Key]
        public int Id_Product { get; set; }

        [StringLength(50)]
        public string Name_Product { get; set; }

        public double? Price_Product { get; set; }

        public int? Inventory_Number { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Column(TypeName = "text")]
        public string Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sales_Bill_Details> Sales_Bill_Details { get; set; }
    }
}
