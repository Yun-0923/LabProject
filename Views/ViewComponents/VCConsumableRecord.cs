using LabProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabProject.Views.ViewComponents
{
    public class VCConsumableRecord:ViewComponent
    {
        private readonly dbLabContext _context;
        public VCConsumableRecord(dbLabContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int CSid)
        {
            var consumableRecord = await _context.ConsumableRecords
                .Include(c => c.Consumable)
                .Include(c => c.Employee)
                .Where(m => m.ConsumableID == CSid).ToListAsync();

            return View(consumableRecord);
        }

    }
}
