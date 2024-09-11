using LabProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabProject.Views.ViewComponents
{
    public class VCChemicalRecord : ViewComponent
    {
        private readonly dbLabContext _context;
        public VCChemicalRecord(dbLabContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int Cid)
        {
            var chemicalRecord = await _context.ChemicalRecords
                .Include(c => c.Chemical)
                .Include(c => c.Employee)
                .Where(m => m.ChemicalID == Cid).ToListAsync();

            return View(chemicalRecord);
        }

    }
}
