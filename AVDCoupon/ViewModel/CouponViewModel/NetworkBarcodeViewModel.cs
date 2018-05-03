using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADVCoupon.ViewModel.CouponViewModel
{
    public class NetworkBarcodeViewModel
    {
        public Guid Id { get; set; }
        public List<Guid> Networks { get; set; }
        public string BarcodeValue { get; set; }
    }
}
