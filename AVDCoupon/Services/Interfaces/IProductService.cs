using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ADVCoupon.Models;

namespace ADVCoupon.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProduct(Guid Id);
        Task<Product> GetProductByCoupon(Guid Id);

        Task<List<Product>> GetProductsAsync();

        Task<Product> CreateProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(Guid Id);

        bool IsExist(Guid Id);
    }
}
