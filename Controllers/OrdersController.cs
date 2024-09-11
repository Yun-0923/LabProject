using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabProject.Models;
using System.Drawing.Printing;

namespace LabProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly dbLabContext _context;

        public OrdersController(dbLabContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            int pageSize = 10; // 每頁顯示的資料數量
            var totalItems = await _context.Orders.CountAsync(); // 總資料數
            // 查詢並應用分頁
            var orders = await _context.Orders
                .Include(o => o.Employee) // 包含關聯的 Employee 資料
                .OrderByDescending(o => o.OrderDate)
                .ThenByDescending(o => o.OrderID)
                .Skip((pageNumber - 1) * pageSize) // 跳過前面的資料
                .Take(pageSize) // 取得當前頁的資料
                .ToListAsync(); // 轉換為列表

            // 將資料傳遞到 View
            ViewBag.Orders = orders; // 當前頁的訂單資料
            ViewBag.CurrentPage = pageNumber; // 當前頁碼
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)totalItems / pageSize); // 總頁數
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Employee)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                ViewData["ErrorMessage"] = "訂單不存在";
                return View(orders);
            }

            if (orders.Status)
            {
                ViewData["ErrorMessage"] = "已完成的訂單無法刪除";
                return View(orders);
            }

            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteChemicalOrderDetails(int orderId, int chemicalId)
        {
            var chemicalOrderDetails = await _context.ChemicalOrderDetails.FindAsync(orderId, chemicalId);
            if (chemicalOrderDetails != null)
            {            
                _context.ChemicalOrderDetails.Remove(chemicalOrderDetails);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Delete", new { id = orderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConsumableOrderDetails(int orderId, int consumableId)
        {
            var consumableOrderDetails = await _context.ConsumableOrderDetails.FindAsync(orderId, consumableId);
            if (consumableOrderDetails != null)
            {
                _context.ConsumableOrderDetails.Remove(consumableOrderDetails);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Delete", new { id = orderId });
        }

    }
}
