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

namespace ADVCoupon.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductCategories
        public async Task<IActionResult> Index()
        {
            var productCategories = await _context.ProductCategories.ToListAsync();
            var productCategoryListViewModel = new List<ProductCategoryViewModel>(productCategories.Count);
            productCategoryListViewModel = productCategories.Select(item => new ProductCategoryViewModel
            {
                Id = item.Id,
                Caption = item.Caption
            }).ToList();
            return View(productCategoryListViewModel);
        }

        // GET: ProductCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategories
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productCategory == null)
            {
                return NotFound();
            }
            var productCategoryModel = new ProductCategoryViewModel()
            {
                Id = productCategory.Id,
                Caption = productCategory.Caption
            };
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
                productCategoryModel.Id = Guid.NewGuid();
                var productCategory = new ProductCategory()
                {
                    Id = productCategoryModel.Id,
                    Caption = productCategoryModel.Caption
                };
                _context.Add(productCategory);
                await _context.SaveChangesAsync();
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

            var productCategory = await _context.ProductCategories.SingleOrDefaultAsync(m => m.Id == id);
            if (productCategory == null)
            {
                return NotFound();
            }
            var productCategoryModel = new ProductCategoryViewModel()
            {
                Id = productCategory.Id,
                Caption = productCategory.Caption
            };
            return View(productCategoryModel);
        }

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,  ProductCategoryViewModel productCategoryModel)
        {
            if (id != productCategoryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var productCategory = new ProductCategory()
                    {
                        Id = productCategoryModel.Id,
                        Caption = productCategoryModel.Caption
                    };
                    _context.Update(productCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(productCategoryModel.Id))
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

            var productCategory = await _context.ProductCategories
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productCategory == null)
            {
                return NotFound();
            }
            var productCategoryModel = new ProductCategoryViewModel()
            {
                Id = productCategory.Id,
                Caption = productCategory.Caption
            };
            return View(productCategoryModel);
        }

        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productCategory = await _context.ProductCategories.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryExists(Guid id)
        {
            return _context.ProductCategories.Any(e => e.Id == id);
        }
    }
}
