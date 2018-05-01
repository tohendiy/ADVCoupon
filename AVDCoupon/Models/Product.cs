using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADVCoupon.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public Provider Provider { get; set; }
        public string BarCode { get; set; }
        public string SKU { get; set; }
        public ProductCategory ProductCategory { get; set; }
        //public List<NetworkBarcode> NetworkBarcodes { get; set; }
        //public double Price { get; set; }
    }
}
