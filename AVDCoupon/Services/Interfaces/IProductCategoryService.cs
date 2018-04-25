using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.ViewModel.ProductCategoryViewModels;

namespace ADVCoupon.Services
{
    public interface IProductCategoryService
    {
        ProductCategory ConvertFromViewModelToProductCategory(ProductCategoryViewModel productCategoryModel);
        ProductCategoryViewModel ConvertFromProductCategoryToViewModel(ProductCategory productCategory);

        Task<ProductCategory> GetProductCategory(Guid Id);
        Task<ProductCategoryViewModel> GetProductCategoryViewModelAsync(Guid Id);

        Task<List<ProductCategory>> GetProductCategoriesAsync();
        Task<List<ProductCategoryViewModel>> GetProductCategoryViewModelsAsync();

        Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory);
        Task<ProductCategory> CreateProductCategoryAsync(ProductCategoryViewModel productCategoryModel);

        Task UpdateProductCategoryAsync(ProductCategory productCategory);
        Task UpdateProductCategoryAsync(ProductCategoryViewModel productCategoryModel);

        Task DeleteProductCategoryAsync(Guid Id);

        bool IsExist(Guid Id);
    }
}
