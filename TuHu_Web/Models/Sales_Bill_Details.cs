namespace TuHu_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sales_Bill_Details
    {
        [Key]
        public int Id_Sales_Bill_Details { get; set; }

        public int? Id_Bill_Of_Sale { get; set; }

        public int? Id_Product { get; set; }

        [StringLength(10)]
        public string Amount { get; set; }

        public double? Price_Bill_Details { get; set; }

        public virtual Bill_Of_Sale Bill_Of_Sale { get; set; }

        public virtual Product Product { get; set; }
    }
}
