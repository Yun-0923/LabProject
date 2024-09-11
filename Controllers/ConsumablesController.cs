using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabProject.Models;

namespace LabProject.Controllers
{
    public class ConsumablesController : Controller
    {
        private readonly dbLabContext _context;

        public ConsumablesController(dbLabContext context)
        {
            _context = context;
        }

        // GET: Consumables
        public async Task<IActionResult> Index()
        {
            var dbLabContext = _context.Consumables.Include(c => c.Supplier);
            return View(await dbLabContext.ToListAsync());
        }

        // GET: Consumables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumables = await _context.Consumables
                .Include(c => c.Supplier)
                .FirstOrDefaultAsync(m => m.ConsumableID == id);
            if (consumables == null)
            {
                return NotFound();
            }

            return View(consumables);
        }

        // POST: Consumables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumables = await _context.Consumables.FindAsync(id);
            if (consumables != null)
            {
                _context.Consumables.Remove(consumables);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsumablesExists(int id)
        {
            return _context.Consumables.Any(e => e.ConsumableID == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConsumableRecord(int id)
        {
            var consumableRecord = await _context.ConsumableRecords.FindAsync(id);
            if (consumableRecord != null)
            {
                var consumable = await _context.Consumables.FirstOrDefaultAsync(c => c.ConsumableID == consumableRecord.ConsumableID);

                if (consumable == null)
                {
                    return NotFound(); // 如果沒有找到對應的耗材，返回404錯誤
                }

                // 更新耗材的數量
                consumable.UnitInStock += consumableRecord.Quantity;
                _context.Update(consumable);
                _context.ConsumableRecords.Remove(consumableRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Delete", new { id = consumableRecord.ConsumableID });
        }

        public async Task<FileContentResult> GetImage(int? cid)
        {
            var consumable = await _context.Consumables.FindAsync(cid);

            if (consumable == null)
            {
                return null;
            }

            return File(consumable.Photo, consumable.PhotoType);
        }
    }
}
