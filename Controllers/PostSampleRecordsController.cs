using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabProject.Models;
using LabProject.ViewModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace LabProject.Controllers
{
    public class PostSampleRecordsController : Controller
    {
        private readonly dbLabContext _context;

        public PostSampleRecordsController(dbLabContext context)
        {
            _context = context;
        }

        
        // GET: PostSampleRecords/Create
        public IActionResult Create(long? SId)
        {
            var activeEmployees = _context.Employee.Where(e => e.IsActive).ToList();
            ViewData["EmployeeID"] = new SelectList(activeEmployees, "EmployeeID", "Name");
            ViewData["SampleID"] = new SelectList(_context.Samples, "SampleID", "SampleName",SId);
            return View();
        }

        // POST: PostSampleRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SampleRecords sampleRecords)
        {
            sampleRecords.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(sampleRecords);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction("Details", "PostSamples", new {id = sampleRecords.SampleID } );
                    return Json(sampleRecords);
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("trigger_error_condition")) 
                    {
                        return Json(sampleRecords);
                    }
                } 
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", sampleRecords.EmployeeID);
            ViewData["SampleID"] = new SelectList(_context.Samples, "SampleID", "SampleName", sampleRecords.SampleID);
            //return View(sampleRecords);
            return Json(sampleRecords);
        }

        public IActionResult GetVCSampleRecord(long? id) 
        {
            return ViewComponent("VCSampleRecord", new { SId = id });
        }
    }
}
