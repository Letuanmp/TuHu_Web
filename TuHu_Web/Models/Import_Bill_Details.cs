namespace TuHu_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Import_Bill_Details
    {
        [Key]
        public int Id_Import_Bill_Details { get; set; }

        public int? Id_Import_Bill { get; set; }

        public int? Id_Imported_Goods { get; set; }

        public double? Number_Imported_Goods { get; set; }

        public double? Price_Bill_Details { get; set; }

        public virtual Import_Bill Import_Bill { get; set; }

        public virtual Imported_Goods Imported_Goods { get; set; }
    }
}
