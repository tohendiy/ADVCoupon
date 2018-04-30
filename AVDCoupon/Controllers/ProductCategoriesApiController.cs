using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using ADVCoupon.Services.Interfaces;
using ADVCoupon.Services;
using ADVCoupon.Models;

namespace ADVCoupon.Controllers
{
    [Route("api/productcategories")]
    public class ProductCategoriesApiController : Controller
    {
        private readonly IProductCategoryService _service;
        public ProductCategoriesApiController(IProductCategoryService service)
        {
            _service = service;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<ProductCategory>>  GetProductCategories()
        {
            var productCategories = await _service.GetProductCategoriesAsync();
            return productCategories;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ProductCategory> GetProductCategory(Guid id)
        {
            var productCategory = await _service.GetProductCategory(id);
            return productCategory;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
