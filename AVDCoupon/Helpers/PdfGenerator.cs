using ADVCoupon.Services.Interfaces;
using DinkToPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.ViewModel.PdfViewModel;

namespace ADVCoupon.Helpers
{
    public static class PdfGenerator
    {
        public static async Task<byte[]> GeneratePDF(ITemplateService templateService)
        {
            var converter = new BasicConverter(new PdfTools());

            string documentContent = await templateService.RenderTemplateAsync(
    "Coupons/CouponPdf", new CouponPdfViewModel() { Message = "test message from code"} );

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
    }
}
