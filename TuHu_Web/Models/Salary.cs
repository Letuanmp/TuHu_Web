namespace TuHu_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Salary")]
    public partial class Salary
    {
        [Key]
        public int Id_Salary { get; set; }

        public int? Id_Staff { get; set; }

        public double? Quantity_Sold { get; set; }

        public double? Basic_Salary { get; set; }

        public int? Working_Days { get; set; }

        public double? Total_Salary { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
