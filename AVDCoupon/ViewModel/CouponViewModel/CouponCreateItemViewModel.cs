using System;
using System.Collections.Generic;
using ADVCoupon.Attributes;
using ADVCoupon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace ADVCoupon.ViewModel.CouponViewModel
{
    public class CouponCreateItemViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Название")]
        [Required]
        public string Caption { get; set; }

        [Display(Name = "Превью")]
        public byte[] ImageView { get; set; }

        [Display(Name = "Лого")]
        public IFormFile Image { get; set; }

        [Display(Name = "Тип скидки")]
        public SelectList DiscountType { get; set; }

        [Display(Name = "Тип скидки")]
        public string DiscountTypeText { get; set; }

        [Display(Name = "Размер скидки")]
        [Required]
        public double Discount { get; set; }

        [Display(Name = "Дата старта")]
        [Required]
        [DateLessThan("EndDate", ErrorMessage = "Not valid")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата конца")]
        [Required]
        public DateTime EndDate { get; set; }

        [Display(Name = "Кол-во купонов")]
        [Required]
        public int TotalCapacity { get; set; }

        public int CurrentCapacity { get; set; }

        [Display(Name = "Подтверждение")]
        public bool IsApproved { get; set; }

        #region Product

        public IEnumerable<Guid> ProductsId { get; set; }

        public MultiSelectList Products { get; set; }

        #endregion

        #region NetworkBarcodes

        public MultiSelectList Networks { get; set; }

        public List<NetworkBarcodeViewModel> NetworkBarcodes { get; set; }

        #endregion

    }
}
