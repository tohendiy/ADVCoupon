using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADVCoupon.Models;
using AVDCoupon.Data;
using ADVCoupon.Services.Interfaces;
using ADVCoupon.Services;
using ADVCoupon.ViewModel.ProductViewModels;

namespace ADVCoupon.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;

        public ProductsController(ApplicationDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;

        }

        // GET: Products
        public async Task<IActionResult> Index()
        { 
            return View();
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var productModel = await _productService.GetProductViewModel(id.Value);
                
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {

            var productModel = await _productService.GetProductWithProviders();

            return View(productModel);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productModel)
        {

            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(productModel);
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);


        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var productModel = await _productService.GetProductViewModel(id.Value);
            if (productModel == null)
            {
                return NotFound();
            }
            return View(productModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productModel)
        {
            if (id != productModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.UpdateProductAsync(productModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(productModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _productService.GetProductViewModel(id.Value);

            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<PartialViewResult> IndexGrid()
        {
            var productsList = await _productService.GetProductViewModels();
            return PartialView("_IndexGrid", productsList);
        }

        private bool ProductExists(Guid id)
        {
            return _productService.IsExist(id);
        }
    }
}
