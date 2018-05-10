using System;
using System.ComponentModel.DataAnnotations;

namespace ADVCoupon.Models
{
    public class Geoposition
    {
        [Key]
        public Guid Id { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Accuracy { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
