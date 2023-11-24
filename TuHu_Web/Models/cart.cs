using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace TuHu_Web.Models
{
    [Table("cart")]
    public partial class cart
    {
        public int id { get; set; }

        public int? idFood { get; set; }

        [StringLength(50)]
        public string userName { get; set; }

        public int? quantity { get; set; }

        public double? totalPrice
        {
            get
            {
                if (quantity != null && quantity > 0)
                {
                    
                        
                   
                        return quantity * food.Price_Product;
                    
                }
                else return 0;
            }
        }

        public virtual Login account { get; set; }

        public virtual Login account1 { get; set; }

        public virtual Login account2 { get; set; }

        public virtual Login account3 { get; set; }

        public virtual Product food { get; set; }

        public virtual Product food1 { get; set; }

        public virtual Product food2 { get; set; }

        public virtual Product food3 { get; set; }
    }
}