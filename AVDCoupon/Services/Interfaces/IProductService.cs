using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.ViewModel.ProductModel;

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

        Task<ProductViewModel> GetProductViewModel(Guid id);

        Task<List<ProductViewModel>> GetProductViewModels();

       

        bool IsExist(Guid Id);
    }
}
