using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectASP.Data;
using ProjectASP.Models;
using PagedList;
using static System.Reflection.Metadata.BlobBuilder;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace ProjectASP.Controllers

{
    public class ProductsController : Controller
    {
        private IHostingEnvironment Environment;
        private readonly Context _context;
         
        public ProductsController(Context context, IHostingEnvironment environment)
        {
            _context = context;
            Environment = environment;
        }
        
        public async Task<IActionResult> Index(int? id = 0, string search = "", int page = 1)
        {
            
            
            var model = _context.Paging(page,search, id);
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            ViewData["Search"] = search;
            return View(model.products);
            //if (page == 1)
            //{
            //    ViewData["Pages"] = model.pages;
            //    ViewData["Page"] = model.page;
            //    return View(model.products);
            //}
            //return View(query);
        }
       
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product = await _context.products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["categoryId"] = new SelectList(_context.categories, "id", "name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,quantity,price,img,categoryId")] Product product, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                Upload(product, img);
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoryId"] = new SelectList(_context.categories, "id", "id", product.categoryId);
            return View(product);
        }
        public string GetDataPath(string file)
        {
            return $"wwwroot\\image\\{file}";
        }

        public void Upload(Product product, IFormFile img)
        {
            if (img != null)
            {
                var path = GetDataPath(img.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                img.CopyTo(stream);
                var imagePath = "/image/" + img.FileName;
                product.img = imagePath;

            }
        }
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["categoryId"] = new SelectList(_context.categories, "id", "name", product.categoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,quantity,price,img,categoryId")] Product product, IFormFile img)
        {
            if (id != product.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Upload(product, img);
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.id))
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
            ViewData["categoryId"] = new SelectList(_context.categories, "id", "id", product.categoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product = await _context.products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.products == null)
            {
                return Problem("Entity set 'Context.products'  is null.");
            }
            var product = await _context.products.FindAsync(id);
            if (product != null)
            {
                _context.products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _context.products.Any(e => e.id == id);
        }
        
    }
}
