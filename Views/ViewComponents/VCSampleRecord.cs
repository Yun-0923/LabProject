using LabProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace LabProject.Views.ViewComponents
{
    public class VCSampleRecord : ViewComponent
    {
        private readonly dbLabContext _context;

        public VCSampleRecord(dbLabContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long Sid)
        {
            var sampleRecord = await _context.SampleRecords
                .Include(c => c.Sample)
                .Include(c => c.Employee)
                .Where(m => m.SampleID == Sid).OrderByDescending(s => s.Date).ToListAsync();

            return View(sampleRecord);
        }
    }
}
