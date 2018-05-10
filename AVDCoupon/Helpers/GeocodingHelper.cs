using ADVCoupon.Models;
using Geocoding;
using Geocoding.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVCoupon.Helpers
{
    public static class GeocodingHelper
    {
        public static async Task<Geoposition> GetCoordinatesByAddressAsync(Geoposition geoposition)
        {
            try
            {
                IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyCDDmQbMj74oDWZoLco5W7t4nMKP8 - 77Qg" };

                var literalAddress = new StringBuilder();
                literalAddress.Append(geoposition.Country)
                    .Append(' ')
                    .Append(geoposition.City)
                    .Append(' ')
                    .Append(geoposition.Region)
                    .Append(' ')
                    .Append(geoposition.Address);

                IEnumerable<Address> addresses = await geocoder.GeocodeAsync(literalAddress.ToString());

                var firstAddress = addresses.First();

                geoposition.Latitude = firstAddress.Coordinates.Latitude.ToString();
                geoposition.Longitude = firstAddress.Coordinates.Longitude.ToString();

                return geoposition;
            }catch(Exception ex)
            {
                return geoposition;
            }
        }
        
    }
}
