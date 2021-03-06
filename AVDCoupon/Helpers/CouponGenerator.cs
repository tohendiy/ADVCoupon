﻿using ADVCoupon.Services.Interfaces;
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
using System.Drawing;
using System.IO;
using NReco.ImageGenerator;

namespace ADVCoupon.Helpers
{
    public static class CouponGenerator
    {
        public static async Task<byte[]> GeneratePDF(ApplicationDbContext context, ITemplateService templateService, ICouponService couponService, Guid couponId, string userId)
        {
            var currentUserCoupon = couponService.GetUserCoupon(userId, couponId);
            var currentCoupon = await couponService.GetCouponById(couponId);

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

    //    public static async Task<byte[]> GenerateImage(ApplicationDbContext context, ITemplateService templateService, ICouponService couponService, Guid couponId, string userId)
    //    {
    //        var currentUserCoupon = couponService.GetUserCoupon(userId, couponId);
    //        var currentCoupon = await couponService.GetCouponById(couponId);
            
    //        string documentContent = await templateService.RenderTemplateAsync(
    //"Coupons/CouponPdf", ConvertUserCouponToPdfViewModel(currentUserCoupon, currentCoupon));
            
    //        var htmlToImageConv = new NReco.ImageGenerator.HtmlToImageConverter();
    //        var jpegBytes = htmlToImageConv.GenerateImage(documentContent, ImageFormat.Jpeg);

    //        return jpegBytes;
    //    }

    //    public static async Task<byte[]> GenerateImageTest(ApplicationDbContext context, ITemplateService templateService, ICouponService couponService)
    //    {
          
    //        string documentContent = await templateService.RenderTemplateAsync(
    //"Coupons/CouponPdf", new CouponPdfViewModel()
    //{
    //    Caption = "TEST"
    //});

    //        var htmlToImageConv = new NReco.ImageGenerator.HtmlToImageConverter();
    //        var jpegBytes = htmlToImageConv.GenerateImage(documentContent, ImageFormat.Jpeg);

    //        return jpegBytes;
    //    }

        //    public static async Task<byte[]> GenerateImage(ApplicationDbContext context, ITemplateService templateService, ICouponService couponService, Guid couponId, string userId)
        //    {
        //        var currentUserCoupon = couponService.GetUserCoupon(context, userId, couponId);
        //        var currentCoupon = await couponService.GetCouponById(context, couponId);

        //        var converter = new BasicConverter(new PdfTools());

        //        string documentContent = await templateService.RenderTemplateAsync(
        //"Coupons/CouponPdf", ConvertUserCouponToPdfViewModel(currentUserCoupon, currentCoupon));

        //        var output = converter.Convert(new HtmlToPdfDocument()
        //        {
        //            Objects =
        //                        {
        //                            new ObjectSettings()
        //                            {
        //                                HtmlContent = documentContent
        //                            }
        //                        }
        //        });

        //        try
        //        {
        //            using (var document = PdfiumViewer.PdfDocument.Load(new MemoryStream(output)))
        //            {
        //                var image = document.Render(0, 300, 300, true);
        //                //image.Save(@"output.png", ImageFormat.Png);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // handle exception here;
        //        }
        //        return output;
        //    }

        public static CouponPdfViewModel ConvertUserCouponToPdfViewModel(UserCoupon userCoupon, Coupon coupon)
        {
            var networkBarcode = coupon.NetworkBarcodes.
            FirstOrDefault(x => x.Networks.FirstOrDefault(y => y.Id == userCoupon.NetworkId) != null);

            var images = networkBarcode.Networks.Select(x => x.LogoImage).ToList();

            var vm = new CouponPdfViewModel
            {
                Caption = coupon.Caption,
                Discount = coupon.Discount,
                DiscountType = coupon.DiscountType,
                EndDate = coupon.EndDate,
                StartDate = coupon.StartDate,
                Image = coupon.Image,
                NetworkImages = images,
                BarcodeLink = BarcodeGenerator.GenerateBarcode(networkBarcode.BarcodeType, networkBarcode.BarcodeValue)
            };
           
            return vm;
        }
    }
}
