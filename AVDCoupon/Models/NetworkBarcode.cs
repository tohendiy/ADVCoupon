using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADVCoupon.Models
{
    public class NetworkBarcode
    {
        [Key]
        public Guid Id { get; set; }
        public List<Network> Networks { get; set; }
        public string BarcodeValue { get; set; }
        
    }
}
