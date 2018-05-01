using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.ViewModel.ProductCategoryViewModels;
using AVDCoupon.Data;
using Microsoft.EntityFrameworkCore;

namespace ADVCoupon.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private ApplicationDbContext _context;
        public ProductCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public ProductCategoryViewModel ConvertFromProductCategoryToViewModel(ProductCategory productCategory)
        {
            var productCategoryModel = new ProductCategoryViewModel()
            {
                Id = productCategory.Id.ToString(),
                Caption = productCategory.Caption
            };
            return productCategoryModel;
        }

        public ProductCategory ConvertFromViewModelToProductCategory(ProductCategoryViewModel productCategoryModel)
        {
            var productCategory = new ProductCategory()
            {
                Id = new Guid(productCategoryModel.Id),
                Caption = productCategoryModel.Caption
            };
            return productCategory;
        }

        public async Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory)
        {
            productCategory.Id = Guid.NewGuid();
            _context.Add(productCategory);
            await _context.SaveChangesAsync();
            return productCategory;
        }

        public async Task<ProductCategory> CreateProductCategoryAsync(ProductCategoryViewModel productCategoryModel)
        {
            var productCategory = new ProductCategory
            {
                Caption = productCategoryModel.Caption,
                Id = Guid.NewGuid(),

            };
            _context.Add(productCategory);
            await _context.SaveChangesAsync();
            return productCategory;

        }

        public async Task DeleteProductCategoryAsync(Guid Id)
        {
            var productCategory = await _context.ProductCategories.SingleOrDefaultAsync(m => m.Id == Id);
            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductCategory> GetProductCategory(Guid Id)
        {
            var productCategory = await _context.ProductCategories
               .SingleOrDefaultAsync(m => m.Id == Id);
            return productCategory;
        }

        public async Task<ProductCategoryViewModel> GetProductCategoryViewModelAsync(Guid Id)
        {
            var productCategory = await _context.ProductCategories
                .SingleOrDefaultAsync(m => m.Id == Id);
            if (productCategory == null)
            {
                return null;
            }
            var productCategoryModel = new ProductCategoryViewModel
            {
                Id = productCategory.Id.ToString(),
                Caption = productCategory.Caption
            };
            return productCategoryModel;
        }

        public async Task<List<ProductCategory>> GetProductCategoriesAsync()
        {
            var productCategorys = await _context.ProductCategories.ToListAsync();
            return productCategorys;
        }

        public async Task<List<ProductCategoryViewModel>> GetProductCategoryViewModelsAsync()
        {
            var productCategories = await _context.ProductCategories.ToListAsync();
            var productCategoriesListViewModel = new List<ProductCategoryViewModel>(productCategories.Count);
            productCategoriesListViewModel = productCategories.Select(item => new ProductCategoryViewModel
            {
                Id = item.Id.ToString(),
                Caption = item.Caption

            }).ToList();
            return productCategoriesListViewModel;
        }

        public bool IsExist(Guid Id)
        {
            return _context.ProductCategories.Any(e => e.Id == Id);
        }

        public async Task UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.Update(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductCategoryAsync(ProductCategoryViewModel productCategoryModel)
        {
            var productCategory = await GetProductCategory(new Guid(productCategoryModel.Id));
            productCategory.Caption = productCategoryModel.Caption;
            _context.Update(productCategory);
            await _context.SaveChangesAsync();
        }
    }
}
