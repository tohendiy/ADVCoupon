using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.ViewModel.ProductViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVCoupon.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProduct(Guid Id);
        Task<Product> GetProductByCoupon(Guid Id);
        Task<ProductViewModel> GetProductViewModel(Guid id);
        Task<ProductViewModel> GetProductWithProviders();

        Task<List<Product>> GetProductsAsync();
        Task<List<ProductViewModel>> GetProductViewModels();


        Task<Product> CreateProductAsync(Product product);
        Task<Product> CreateProductAsync(ProductViewModel productModel);

        Task UpdateProductAsync(Product product);
        Task UpdateProductAsync(ProductViewModel productModel);


        Task DeleteProductAsync(Guid Id);


        SelectList GetSelectListProviders();
       
        bool IsExist(Guid Id);
    }
}
