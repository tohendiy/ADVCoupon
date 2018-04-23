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

        public const string SMTP_EMAIL_NAME = "ADV Coupon";
        public const string SMTP_EMAIL_FROM = "user.atc28@gmail.com";
        public const string SMTP_EMAIL_PASSWORD = "A80962432409";

        public const string SMTP_SERVER = "smtp.gmail.com";
        public const int SMTP_PORT = 465;
        public const bool SMTP_SSL = true;


        #region Barcode generation

        public const string BARCODE_GENERATION_URL = "http://www.barcodes4.me/barcode/";

        #endregion


    }
}
