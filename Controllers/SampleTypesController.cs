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
    public class SampleTypesController : Controller
    {
        private readonly dbLabContext _context;

        public SampleTypesController(dbLabContext context)
        {
            _context = context;
        }

        // GET: SampleTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SampleType.ToListAsync());
        }

        // GET: SampleTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sampleType = await _context.SampleType
                .FirstOrDefaultAsync(m => m.TypeID == id);
            if (sampleType == null)
            {
                return NotFound();
            }

            return View(sampleType);
        }

        // POST: SampleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sampleType = await _context.SampleType.FindAsync(id);
            if (sampleType != null)
            {
                _context.SampleType.Remove(sampleType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SampleTypeExists(int id)
        {
            return _context.SampleType.Any(e => e.TypeID == id);
        }
    }
}
