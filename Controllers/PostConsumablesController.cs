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
    public class PostConsumablesController : Controller
    {
        private readonly dbLabContext _context;

        public PostConsumablesController(dbLabContext context)
        {
            _context = context;
        }

        // GET: PostConsumables
        public async Task<IActionResult> Index()
        {
            var dbLabContext = _context.Consumables.Include(c => c.Supplier);
            return View(await dbLabContext.ToListAsync());
        }

        // GET: PostConsumables/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: PostConsumables/Create
        public IActionResult Create()
        {
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "CompanyName");
            return View();
        }

        // POST: PostConsumables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Consumables consumables,IFormFile? uploadPhoto)
        {
            if(uploadPhoto != null)
            {
                if (uploadPhoto.ContentType != "image/jpeg" && uploadPhoto.ContentType != "image/png") 
                {
                    ViewData["Error"] = "請上傳 jpg 或 png 格式的檔案";
                    return View(consumables);
                }

                var mem = new MemoryStream(); //開記憶體空間
                uploadPhoto.CopyTo(mem); //把上傳的照片放進空間處理

                consumables.Photo = mem.ToArray(); //轉成陣列丟回photo欄位
                consumables.PhotoType = uploadPhoto.ContentType;
            }

            if (ModelState.IsValid)
            {
                _context.Add(consumables);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "CompanyName", consumables.SupplierID);
            return View(consumables);
        }

        private bool ConsumablesExists(int id)
        {
            return _context.Consumables.Any(e => e.ConsumableID == id);
        }
    }
}
