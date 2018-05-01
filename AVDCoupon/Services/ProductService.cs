using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.Services.Interfaces;
using ADVCoupon.ViewModel.ProductModel;
using AVDCoupon.Data;
using Microsoft.EntityFrameworkCore;

namespace ADVCoupon.Services
{
    public class ProductService : IProductService
    {
        private ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            product.Id = Guid.NewGuid();
            _context.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(Guid Id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == Id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProduct(Guid Id)
        {
            var product = await _context.Products
               .SingleOrDefaultAsync(m => m.Id == Id);
            return product;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<ProductViewModel> GetProductViewModel(Guid id)
        {
            var p = await _context.Products.SingleOrDefaultAsync(m => m.Id == id); 
            
            return new ProductViewModel() { Id = p.Id.ToString(), BarCode = p.BarCode, Image = p.Image, Name = p.Name, ProductCategoryName = p.ProductCategory.Caption, SKU = p.SKU, SupplierName = p.Provider.Name };
            
        }

        public async Task<List<ProductViewModel>> GetProductViewModels()
        {
            var productsList = await _context.Products.ToListAsync();

            var products = from p in productsList
                           select new ProductViewModel() { Id = p.Id.ToString(), BarCode = p.BarCode, Image = p.Image, Name = p.Name, ProductCategoryName = p.ProductCategory.Caption, SKU = p.SKU, SupplierName = p.Provider.Name };

            return products.ToList();
        }

        public bool IsExist(Guid Id)
        {
            return _context.Products.Any(e => e.Id == Id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
