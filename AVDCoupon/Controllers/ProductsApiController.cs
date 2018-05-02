using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using ADVCoupon.Services.Interfaces;
using ADVCoupon.Models;

namespace ADVCoupon.Controllers
{
    [Route("api/products")]
    public class ProductsApiController : Controller
    {
        private readonly IProductService _service;
        public ProductsApiController(IProductService service)
        {
            _service = service;
        }
        // GET: api/values
        [HttpGet]
        public async Task<List<Product>> GetProducts()
        {
            var products = await _service.GetProductsAsync();
            return products;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Product> GetProduct(Guid id)
        {
            var product = await _service.GetProduct(id);
            return product;
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
