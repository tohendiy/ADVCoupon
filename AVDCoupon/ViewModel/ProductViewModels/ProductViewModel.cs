using ADVCoupon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ADVCoupon.ViewModel.ProductViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Штрихкод")]
        [Required]
        public string BarCode { get; set; }
        [Display(Name = "Название")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Поставщик")]
        public string SupplierName { get; set; }

        [Display(Name = "СКУ")]
        [Required]
        public string SKU { get; set; }

        [Display(Name = "Логотип")]
        public byte[] Image { get; set; }

        [Display(Name = "Логотип")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Поставщики")]
        public SelectList Providers { get; set; }
        public Guid ProviderId { get; set; }
    }
}
