using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADVCoupon.Helpers
{
    public static class CookiesHelper
    {
        public static string GetGeolocation(Microsoft.AspNetCore.Http.HttpContext context)
        {
            try
            {
                string latitude, longitude, accuracy;
                
                latitude = context.Request.Cookies["latitude"];
                context.Response.Cookies.Delete("latitude");
                
                longitude = context.Request.Cookies["longitude"];
                context.Response.Cookies.Delete("longitude");

                accuracy = context.Request.Cookies["accuracy"];
                context.Response.Cookies.Delete("accuracy");

                var locationObj = new
                {
                    longitude,
                    latitude,
                    accuracy
                };

                return JsonConvert.SerializeObject(locationObj).ToString();
            }
            catch (Exception ex)
            {
                return "Unable to locate user";
            }
        }
    }
}
