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
    public class PostSamplesController : Controller
    {
        private readonly dbLabContext _context;

        public PostSamplesController(dbLabContext context)
        {
            _context = context;
        }

        // GET: PostSamples
        public async Task<IActionResult> Index(int typeid = 1)
        {
            VMPostSamples postSamples = new VMPostSamples()
            {
                Sample = await _context.Samples.Where(s => s.TypeID == typeid).OrderBy(s => s.SampleName).ToListAsync(),
                Sampletype = await _context.SampleType.ToListAsync()
            };
            ViewData["TypeName"] = _context.SampleType.Find(typeid).TypeName;
            ViewData["TypeID"] = typeid;

            return View(postSamples);

        }

        // GET: PostSamples/Details/5
        public async Task<IActionResult> Details(long? id)
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

        // GET: PostSamples/Create
        public IActionResult Create(int typeid = 1)
        { 
            ViewData["SampleTypeList"] = new SelectList(_context.SampleType, "TypeID", "TypeName", typeid);
            ViewData["TypeID"] = typeid;
            return View();
        }

        // POST: PostSamples/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Samples sam)
        {
            ViewData["SampleTypeList"] = new SelectList(_context.SampleType, "TypeID", "TypeName", sam.TypeID);
            if (sam==null) 
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                _context.Add(sam);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { typeid = sam.TypeID });
            }

           
            return View(sam);
        }

        public IActionResult SampleTypeCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SampleTypeCreate(SampleType sampleType)
        {
            
            if (sampleType == null)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                _context.Add(sampleType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { typeid = sampleType.TypeID });
            }

            return View(sampleType);
        }

        private bool SamplesExists(long id)
        {
            return _context.Samples.Any(e => e.SampleID == id);
        }
    }
}
