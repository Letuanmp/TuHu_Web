namespace TuHu_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Bill_Of_Sale = new HashSet<Bill_Of_Sale>();
        }

        [Key]
        public int Id_Customer { get; set; }

        [StringLength(50)]
        public string Name_Customer { get; set; }

        [StringLength(50)]
        public string Address_Customer { get; set; }

        public int? Phone_Number_Customer { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill_Of_Sale> Bill_Of_Sale { get; set; }
    }
}
