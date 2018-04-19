using System;
using System.ComponentModel.DataAnnotations;

namespace ADVCoupon.Models
{
    public class ProductCategory
    {

        [Key]
        public Guid Id { get; set; }
        public string Caption { get; set; }

    }
}
