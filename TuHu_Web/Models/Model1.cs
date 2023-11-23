using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TuHu_Web.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Bill_Of_Sale> Bill_Of_Sale { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Import_Bill> Import_Bill { get; set; }
        public virtual DbSet<Import_Bill_Details> Import_Bill_Details { get; set; }
        public virtual DbSet<Imported_Goods> Imported_Goods { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<Sales_Bill_Details> Sales_Bill_Details { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Sales_Bill_Details>()
                .Property(e => e.Amount)
                .IsFixedLength();
        }
    }
}
