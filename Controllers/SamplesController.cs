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
    public class SamplesController : Controller
    {
        private readonly dbLabContext _context;

        public SamplesController(dbLabContext context)
        {
            _context = context;
        }

        // GET: Samples
        public async Task<IActionResult> Index()
        {
            var dbLabContext = _context.Samples.Include(s => s.SampleType).OrderBy(s =>s.SampleName).ThenBy(s=>s.SampleType);
            return View(await dbLabContext.ToListAsync());
        }

        // GET: Samples/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samples = await _context.Samples
                .Include(s => s.SampleType)
                .FirstOrDefaultAsync(m => m.SampleID == id);
            if (samples == null)
            {
                return NotFound();
            }

            return View(samples);
        }

        // POST: Samples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var samples = await _context.Samples.FindAsync(id);
            if (samples != null)
            {
                _context.Samples.Remove(samples);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //增加刪除使用紀錄Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSampleRecord(int id)
        {
            var sampleRecord = await _context.SampleRecords.FindAsync(id);
            
            if (sampleRecord != null)
            {
                var sample = await _context.Samples.FirstOrDefaultAsync(s => s.SampleID == sampleRecord.SampleID);
                if (sample == null)
                {
                    return NotFound();
                }
                if (sampleRecord.TransactionType)
                {
                    sample.Quantity -= sampleRecord.Quantity;
                }
                else { sample.Quantity += sampleRecord.Quantity; }
               
                _context.Samples.Update(sample);
                _context.SampleRecords.Remove(sampleRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Delete", new { id = sampleRecord.SampleID });
        }

        private bool SamplesExists(long id)
        {
            return _context.Samples.Any(e => e.SampleID == id);
        }
    }
}
