using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADVCoupon.Helpers
{
    public static class Constants
    {
        public const string ADMIN_ROLE = "admin";
        public const string USER_ROLE = "user";
        public const string MERCHANT_ROLE = "merchant";
        public const string SUPPLIER_ROLE = "supplier";

        public const string SMTP_EMAIL_NAME = "ADV Coupon";
        public const string SMTP_EMAIL_FROM = "user.atc28@gmail.com";
        public const string SMTP_EMAIL_PASSWORD = "A80962432409";

        public const string SMTP_SERVER = "smtp.gmail.com";
        public const int SMTP_PORT = 465;
        public const bool SMTP_SSL = true;


        public const string DISCOUNT_TYPE_PERCENT = "%";
        public const string DISCOUNT_TYPE_ABSOLUTE = "грн.";


        #region Barcode generation

        public const string BARCODE_TYPE_EAN13 = "EAN13";
        public const string BARCODE_TYPE_EAN8 = "EAN8";
        public const string BARCODE_TYPE_CODE128 = "Code128";

        public const string BARCODE_GENERATION_URL = "https://barcode.tec-it.com/barcode.ashx?dpi=96&";
        //https://barcode.tec-it.com/barcode.ashx?data=123121212312&code=EAN13&dpi=96
        #endregion


    }
}
