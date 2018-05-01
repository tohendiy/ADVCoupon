using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ADVCoupon.Services;
using AVDCoupon.Models;

namespace ADVCoupon.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/coupons")]
    public class CouponsApiController : Controller
    {
        private readonly ICouponService _service;
        public CouponsApiController(ICouponService service)
        {
            _service = service;
        }
        // GET: api/values
        [HttpGet]
        public async Task<List<Coupon>> Get()
        {
            var values = await _service.GetCouponsAsync();
            return values;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Coupon> GetCoupon(Guid id)
        {
            var coupon = await _service.GetCouponAsync(id);
            return coupon;
        }

        //[HttpPost]
        //public async Task GenerateCoupon([FromBody])
        //{
            
        //}

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[HttpPost]
        //public async Task GenerateCouponWithUser([FromBody])
        //{

        //}
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<Coupon>> GetCouponsByUser(string idUser)
        {
            var userCoupons = await _service.GetCouponsByUserAsync(idUser);
            return userCoupons;
                                        
        }

        public async Task<IEnumerable<Coupon>> GetCouponsByNetwork(Guid idNetwork)
        {
            var networkCoupons = await _service.GetCouponsByNetworkAsync(idNetwork);
            return networkCoupons;

        }
        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

       
    }
}
