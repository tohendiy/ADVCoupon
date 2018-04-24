using ADVCoupon.Services.Interfaces;
using DinkToPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.ViewModel.PdfViewModel;
using ADVCoupon.Services;
using AVDCoupon.Data;
using ADVCoupon.Models;
using AVDCoupon.Models;

namespace ADVCoupon.Helpers
{
    public static class PdfGenerator
    {
        public static async Task<byte[]> GeneratePDF(ApplicationDbContext context, ITemplateService templateService, ICouponService couponService, Guid couponId, string userId)
        {
            var currentUserCoupon = couponService.GetUserCoupon(context, userId, couponId);
            var currentCoupon = await couponService.GetCouponById(context, couponId);

            var converter = new BasicConverter(new PdfTools());

            string documentContent = await templateService.RenderTemplateAsync(
    "Coupons/CouponPdf", ConvertUserCouponToPdfViewModel(currentUserCoupon, currentCoupon));

            var output = converter.Convert(new HtmlToPdfDocument()
            {
                Objects =
                            {
                                new ObjectSettings()
                                {
                                    HtmlContent = documentContent
                                }
                            }
            });

            return output;

        }

        private static CouponPdfViewModel ConvertUserCouponToPdfViewModel(UserCoupon userCoupon, Coupon coupon)
        {
            var networkBarcode = coupon.Product.NetworkBarcodes.
                FirstOrDefault(x => x.Networks.FirstOrDefault(y => y.Id == userCoupon.NetworkId) != null).BarcodeValue;

            var vm = new CouponPdfViewModel
            {
                Caption = coupon.Caption,
                DiscountAbsolute = coupon.DiscountAbsolute,
                DiscountPercentage = coupon.DiscountPercentage,
                EndDate = coupon.EndDate,
                NetworkCoupons = coupon.NetworkCoupons,
                Product = coupon.Product,
                StartDate = coupon.StartDate,
                BarcodeLink = Helpers.BarcodeGenerator.GenerateBarcode("c128c", networkBarcode),
                Barcode = networkBarcode
            };

            return vm;
        }
    }
}
