using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADVCoupon.ViewModel.CouponViewModel
{
    public class NetworkBarcodeViewModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        [Display(Name = "Сети")]
        public List<Guid> Networks { get; set; }
        [Display(Name = "Штрихкод")]
        [Required]
        public string BarcodeValue { get; set; }
        [Display(Name = "Тип")]
        [Required]
        public string BarcodeType { get; set; }
    }
}
