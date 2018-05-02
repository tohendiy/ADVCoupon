using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.Services.Interfaces;
using ADVCoupon.ViewModel.ProductViewModels;
using AVDCoupon.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var product = await _context.Products.Include(item => item.Provider)
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
            var p = await _context.Products.Include(item => item.Provider).SingleOrDefaultAsync(m => m.Id == id); 
            
            return new ProductViewModel() { Id = p.Id, 
                BarCode = p.BarCode, 
                Image = p.Image, 
                Name = p.Name, 
                SKU = p.SKU, 
                SupplierName = p.Provider.Name,
                ProviderId = p.Provider.Id,
                Providers = GetSelectListProviders()
            };
            
        }

        public async Task<List<ProductViewModel>> GetProductViewModels()
        {
            var productsList = await _context.Products.Include(item => item.Provider).ToListAsync();

            var products = from p in productsList
                           select new ProductViewModel() { Id = p.Id, 
                BarCode = p.BarCode, 
                Image = p.Image, 
                Name = p.Name, 
                SKU = p.SKU, 
                SupplierName = p.Provider.Name };

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
        public async Task<Product> GetProductByCoupon(Guid Id)
        {
            var product = await _context.Products.Where(item => item.Coupon.Id == Id).FirstOrDefaultAsync();
            return product;
        }

        public async Task<ProductViewModel> GetProductWithProviders()
        {
            var productModel = new ProductViewModel();
            productModel.Providers = GetSelectListProviders();
            return productModel;
        }

        public SelectList GetSelectListProviders()
        {
            var providers = _context.Providers.Select(x => new { Id = x.Id, Value = x.Name });

            var providersSelectList = new SelectList(providers, "Id", "Value");
            return providersSelectList;
        }

        public async Task UpdateProductAsync(ProductViewModel productModel)
        {
            var product = await GetProduct(productModel.Id);
            product.Name = productModel.Name;
            product.BarCode = productModel.BarCode;
            product.SKU = productModel.SKU;
            product.Provider = _context.Providers.FirstOrDefault(item => item.Id == productModel.ProviderId);

            if (productModel.ImageFile != null)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await productModel.ImageFile.CopyToAsync(memoryStream);
                    if (memoryStream != null)
                    {
                        product.Image = memoryStream.ToArray();
                    }
                }
            }
            _context.Update(product);
            await _context.SaveChangesAsync();
        }



        public async Task<Product> CreateProductAsync(ProductViewModel productModel)
        {
            var product = new Product
            {
                Name = productModel.Name,
                Id = Guid.NewGuid(),
                BarCode = productModel.BarCode,
                SKU = productModel.SKU,
                Provider = _context.Providers.FirstOrDefault(item => item.Id == productModel.ProviderId)

            };
            if (productModel.ImageFile != null)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await productModel.ImageFile.CopyToAsync(memoryStream);
                    if (memoryStream != null)
                    {
                        product.Image = memoryStream.ToArray();
                    }
                }
            }
            _context.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
