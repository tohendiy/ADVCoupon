using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADVCoupon.Models;
using AVDCoupon.Data;
using ADVCoupon.ViewModel.ProductCategoryViewModels;
using ADVCoupon.Services;
using Microsoft.AspNetCore.Authorization;
using ADVCoupon.Helpers;

namespace ADVCoupon.Controllers
{
    [Authorize(Roles = Constants.ADMIN_ROLE)]
    public class ProductCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductCategoryService _service;

        public ProductCategoriesController(ApplicationDbContext context, IProductCategoryService service)
        {
            _context = context;
            _service = service;
        }

        // GET: ProductCategories
        public async Task<IActionResult> Index()
        {
            var productCategoriesModel = await _service.GetProductCategoryViewModelsAsync();
            return View(productCategoriesModel);       
        }

        // GET: ProductCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategoryModel = await _service.GetProductCategoryViewModelAsync(id.Value);
            if (productCategoryModel == null)
            {
                return NotFound();
            }
            return View(productCategoryModel);
        }

        // GET: ProductCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCategoryViewModel productCategoryModel)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateProductCategoryAsync(productCategoryModel);

                return RedirectToAction(nameof(Index));
            }
            return View(productCategoryModel);
        }

        // GET: ProductCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategoryModel = await _service.GetProductCategoryViewModelAsync(id.Value);
            if (productCategoryModel == null)
            {
                return NotFound();
            }
            return View(productCategoryModel);
        }

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,  ProductCategoryViewModel productCategoryModel)
        {
            if (id != new Guid(productCategoryModel.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateProductCategoryAsync(productCategoryModel);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(new Guid(productCategoryModel.Id)))
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
            return View(productCategoryModel);
        }

        // GET: ProductCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategoryModel = await _service.GetProductCategoryViewModelAsync(id.Value);
            if (productCategoryModel == null)
            {
                return NotFound();
            }
            return View(productCategoryModel);
        }

        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteProductCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<PartialViewResult> IndexGrid()
        {
            var productCategoriesModel = await _service.GetProductCategoryViewModelsAsync();

            return PartialView("_IndexGrid", productCategoriesModel);
        }

        private bool ProductCategoryExists(Guid id)
        {
            return _service.IsExist(id);
        }
    }
}
