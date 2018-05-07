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
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        [Display(Name = "Тип скидки")]
        public SelectList DiscountType { get; set; }

        [Display(Name = "Тип скидки")]
        [Required]
        public string DiscountTypeText { get; set; }

        [Display(Name = "Размер скидки")]
        [Required]
        [Range(0, double.PositiveInfinity)]
        public double Discount { get; set; }

        [Display(Name = "Дата старта")]
        [Required]
        [DateLessThan("EndDate", ErrorMessage = "Not valid")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата конца")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Кол-во купонов")]
        [Required]
        [Range(0, int.MaxValue)]
        public int TotalCapacity { get; set; }

        [Display(Name = "Использованное кол-во купонов")]
        [Range(0, int.MaxValue)]
        public int CurrentCapacity { get; set; }

        [Display(Name = "Подтверждение")]
        public bool IsApproved { get; set; }

        #region Product
        [Display(Name = "Продукты")]
        public IEnumerable<Guid> ProductsId { get; set; }

        [Display(Name = "Продукты")]
        public MultiSelectList Products { get; set; }

        #endregion

        #region NetworkBarcodes
        [Display(Name = "Сети")]
        public MultiSelectList Networks { get; set; }

        [Display(Name = "Штрихкоды сетей")]
        public List<NetworkBarcodeViewModel> NetworkBarcodes { get; set; }

        #endregion

    }
}
