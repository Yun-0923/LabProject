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
    public class ChemicalsController : Controller
    {
        private readonly dbLabContext _context;

        public ChemicalsController(dbLabContext context)
        {
            _context = context;
        }

        // GET: Chemicals
        public async Task<IActionResult> Index()
        {
            var dbLabContext = _context.Chemicals.Include(c => c.Categories).Include(c => c.Supplier);
            return View(await dbLabContext.ToListAsync());
        }

        // GET: Chemicals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chemicals = await _context.Chemicals
                .Include(c => c.Categories)
                .Include(c => c.Supplier)
                .FirstOrDefaultAsync(m => m.ChemicalID == id);
            if (chemicals == null)
            {
                return NotFound();
            }

            return View(chemicals);
        }

        // POST: Chemicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chemicals = await _context.Chemicals.FindAsync(id);
            if (chemicals != null)
            {
                _context.Chemicals.Remove(chemicals);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //增加刪除使用紀錄Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteChemicalRecord(int id)
        {
            var chemicalRecord = await _context.ChemicalRecords.FindAsync(id);

            if (chemicalRecord != null)
            {
                var chemical = await _context.Chemicals.FirstOrDefaultAsync(c => c.ChemicalID== chemicalRecord.ChemicalID);

                if (chemical == null)
                {
                    return NotFound(); // 如果沒有找到對應的化學品，返回404錯誤
                }

                //更新化學品數量
                chemical.Stock = Math.Round(chemical.Stock + chemicalRecord.Quantity,1);
                _context.Chemicals.Update(chemical);
                _context.ChemicalRecords.Remove(chemicalRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Delete", new { id = chemicalRecord.ChemicalID });
        }

        private bool ChemicalsExists(int id)
        {
            return _context.Chemicals.Any(e => e.ChemicalID == id);
        }
    }
}
