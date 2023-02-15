using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectASP.Data;
using ProjectASP.Models;

namespace ProjectASP.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly Context _context;

        public OrderDetailsController(Context context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var context = _context.orderDetails.Include(o => o.order).Include(o => o.product);
            return View(await context.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.orderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.orderDetails
                .Include(o => o.order)
                .Include(o => o.product)
                .FirstOrDefaultAsync(m => m.id == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            ViewData["orderId"] = new SelectList(_context.orders, "id", "code");
            ViewData["productId"] = new SelectList(_context.products, "id", "name");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,orderId,productId,quantity")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["orderId"] = new SelectList(_context.orders, "id", "id", orderDetail.orderId);
            ViewData["productId"] = new SelectList(_context.products, "id", "name", orderDetail.productId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.orderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.orderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            ViewData["orderId"] = new SelectList(_context.orders, "id", "code", orderDetail.orderId);
            ViewData["productId"] = new SelectList(_context.products, "id", "name", orderDetail.productId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,orderId,productId,quantity")] OrderDetail orderDetail)
        {
            if (id != orderDetail.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.id))
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
            ViewData["orderId"] = new SelectList(_context.orders, "id", "id", orderDetail.orderId);
            ViewData["productId"] = new SelectList(_context.products, "id", "name", orderDetail.productId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.orderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.orderDetails
                .Include(o => o.order)
                .Include(o => o.product)
                .FirstOrDefaultAsync(m => m.id == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.orderDetails == null)
            {
                return Problem("Entity set 'Context.orderDetails'  is null.");
            }
            var orderDetail = await _context.orderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                _context.orderDetails.Remove(orderDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
          return _context.orderDetails.Any(e => e.id == id);
        }
    }
}
