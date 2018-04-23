using System;
using System.Text;

namespace ADVCoupon.Helpers
{
    public static class BarcodeGenerator
    {
        public static string GenerateBarcode(string barcodeType, string value)
        {
            return Constants.BARCODE_GENERATION_URL + barcodeType + "/" + value + ".jpeg";
        }
    }
}
