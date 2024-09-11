using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabProject.Models;
using LabProject.ViewModels;
using Microsoft.CodeAnalysis;

namespace LabProject.Controllers
{
    public class PostChemicalsController : Controller
    {
        private readonly dbLabContext _context;

        public PostChemicalsController(dbLabContext context)
        {
            _context = context;
        }

        // GET: PostChemicals
        public async Task<IActionResult> Index(int catid=3)
        {
            VMPostChemicals chemical = new VMPostChemicals()
            {
                Chemical = await _context.Chemicals.Include(c => c.Supplier).Where(c => c.CategoryID == catid)
                .OrderBy(c => c.EnglishName).ThenBy(c => c.ChineseName).ToListAsync(),
                Category = await _context.Categories.ToListAsync()
            };

            ViewData["CatName"]= _context.Categories.Find(catid).CategoryName;
            ViewData["CatID"] = catid;
            
            return View(chemical);
        }

        // GET: PostChemicals/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: PostChemicals/Create
        public IActionResult Create(int catid = 1)
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", catid);
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "CompanyName");
            ViewData["CatID"] = catid;
            return View();
        }

        // POST: PostChemicals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Chemicals chemicals)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chemicals);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { catid= chemicals.CategoryID });
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", chemicals.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "CompanyName", chemicals.SupplierID);
            return View(chemicals);
        }

        // GET: PostChemicals/Create
        public IActionResult CategoryCreate()
        {
            return View();
        }

        // POST: PostChemicals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate(Categories categories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categories);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { catid = categories.CategoryID });
            }
            return View(categories);
        }




        private bool ChemicalsExists(int id)
        {
            return _context.Chemicals.Any(e => e.ChemicalID == id);
        }


        // GET: PostChemicalRecords/Create
        public IActionResult PostChemicalRecord(int? CId)
        {
            var activeEmployees = _context.Employee.Where(e => e.IsActive).ToList();
            ViewData["EmployeeID"] = new SelectList(activeEmployees, "EmployeeID", "Name");
            ViewData["ChemicalID"] = new SelectList(_context.Chemicals, "ChemicalID", "ChineseName", CId);
            return View();
        }

        // POST: PostChemicalRecords/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostChemicalRecord(ChemicalRecords chemicalRecords)
        {
            chemicalRecords.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                try 
                {
                    _context.Add(chemicalRecords);
                    await _context.SaveChangesAsync();
                    return Json(chemicalRecords);
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("trigger_error_condition"))
                    {
                        return Json(chemicalRecords);
                    }
                }
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", chemicalRecords.EmployeeID);
            ViewData["ChemicalID"] = new SelectList(_context.Chemicals, "ChemicalID", "ChineseName", chemicalRecords.ChemicalID);
            return Json(chemicalRecords);
        }

        public IActionResult GetVCChemicalRecord(int? id)
        {
            return ViewComponent("VCChemicalRecord", new { CId = id });
        }


    }
}
