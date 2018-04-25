using System;
using System.Text;

namespace ADVCoupon.Helpers
{
    public static class BarcodeGenerator
    {
        public static string GenerateBarcode(string barcodeType, string value)
        {
            return Constants.BARCODE_GENERATION_URL + "code=" + barcodeType + "&data=" + value;
        }
        //https://barcode.tec-it.com/barcode.ashx?data=123121212312&code=EAN13&dpi=96

    }
}
