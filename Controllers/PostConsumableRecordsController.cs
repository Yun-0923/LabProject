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
    public class PostConsumableRecordsController : Controller
    {
        private readonly dbLabContext _context;

        public PostConsumableRecordsController(dbLabContext context)
        {
            _context = context;
        }


        // GET: PostConsumableRecords/Create
        public IActionResult Create(int? CSid)
        {
            ViewData["ConsumableID"] = new SelectList(_context.Consumables, "ConsumableID", "ConsumableName", CSid);
            var activeEmployees = _context.Employee.Where(e => e.IsActive).ToList();
            ViewData["EmployeeID"] = new SelectList(activeEmployees, "EmployeeID", "Name");
            return View();
        }

        // POST: PostConsumableRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsumableRecords consumableRecords)
        {
            consumableRecords.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    // 嘗試新增或更新資料
                    _context.Add(consumableRecords);
                    await _context.SaveChangesAsync();
                    return Json(consumableRecords);
                }
                catch (DbUpdateException ex)
                {
                    // 捕獲 SQL 觸發器產生的錯誤訊息
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("trigger_error_condition")) 
                    {
                        return Json(consumableRecords);
                    }
                }
            }
            
            ViewData["ConsumableID"] = new SelectList(_context.Consumables, "ConsumableID", "ConsumableName", consumableRecords.ConsumableID);
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", consumableRecords.EmployeeID);
            return Json(consumableRecords);
        }
        public IActionResult GetVCConsumableRecord(int? id)
        {
            return ViewComponent("VCConsumableRecord", new { CSid = id });
        }

        private bool ConsumableRecordsExists(int id)
        {
            return _context.ConsumableRecords.Any(e => e.RecordID == id);
        }
    }
}
