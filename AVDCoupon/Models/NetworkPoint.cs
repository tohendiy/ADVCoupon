using System;
using System.ComponentModel.DataAnnotations;

namespace ADVCoupon.Models
{
    public class NetworkPoint
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Geoposition Geoposition { get; set; }
    }
}
