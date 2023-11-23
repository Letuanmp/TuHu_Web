namespace TuHu_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            Bill_Of_Sale = new HashSet<Bill_Of_Sale>();
            Import_Bill = new HashSet<Import_Bill>();
            Salaries = new HashSet<Salary>();
        }

        [Key]
        public int Id_Staff { get; set; }

        [StringLength(50)]
        public string Name_Of_Staff { get; set; }

        public int? Phone_Number_Of_Staff { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_Of_Birth { get; set; }

        [StringLength(50)]
        public string Native_Place { get; set; }

        public int? Age { get; set; }

        [StringLength(50)]
        public string Accommodation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill_Of_Sale> Bill_Of_Sale { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Import_Bill> Import_Bill { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Salary> Salaries { get; set; }
    }
}
