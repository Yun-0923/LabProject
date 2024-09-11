using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabProject.Models;
using LabProject.ViewModels;

namespace LabProject.Controllers
{
    public class PostOrdersController : Controller
    {
        private readonly dbLabContext _context;

        public PostOrdersController(dbLabContext context)
        {
            _context = context;
        }

        // GET: PostOrders
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            //var dbLabContext = _context.Orders.Include(o => o.Employee).OrderByDescending(o => o.OrderDate).ThenByDescending(o => o.OrderID).Take(10);
            //return View(await dbLabContext.ToListAsync());

            int pageSize = 10; // 每頁顯示的資料數量
            var totalItems = await _context.Orders.CountAsync(); // 總資料數

            // 查詢並應用分頁
            var orders = await _context.Orders
                .Include(o => o.Employee) // 包含關聯的 Employee 資料
                .OrderByDescending(o => o.OrderDate)
                .ThenByDescending(o => o.OrderID)
                .Skip((pageNumber - 1) * pageSize) // 跳過前面的資料
                .Take(pageSize) // 取得當前頁的資料
                .ToListAsync(); // 轉換為列表

            // 將資料傳遞到 View
            ViewBag.Orders = orders; // 當前頁的訂單資料
            ViewBag.CurrentPage = pageNumber; // 當前頁碼
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)totalItems / pageSize); // 總頁數

            return View(orders);
        }

        // GET: PostOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Employee)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: PostOrders/Create
        public IActionResult Create()
        {
            ViewData["OrderDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            var activeEmployees = _context.Employee.Where(e => e.IsActive).ToList();
            ViewData["EmployeeID"] = new SelectList(activeEmployees, "EmployeeID", "Name");
            return View();
        }

        // POST: PostOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = orders.OrderID });
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", orders.EmployeeID);
            return View(orders);
        }

        // GET: PostOrders/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", orders.EmployeeID);
            ViewData["OrderID"] = orders.OrderID;
            return View(orders);
        }

        // POST: PostOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Orders orders)
        {
            if (id != orders.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    orders.DeliveryDate = DateTime.Now;
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return Json(orders);
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", orders.EmployeeID);
            return Json(orders);
        }

 
        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }


        // GET: PostOrders/Create
        public IActionResult CreateChemicalDetails(int orderId)
        {

            ViewData["ChemicalID"] = new SelectList(_context.Chemicals, "ChemicalID", "ChineseName");
            ViewBag.OrderID = orderId;
            return View();
        }

        // POST: PostOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateChemicalDetails(ChemicalOrderDetails chemicalOrderDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chemicalOrderDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = chemicalOrderDetails.OrderID });
            }
            ViewData["ChemicalID"] = new SelectList(_context.Chemicals, "ChemicalID", "ChineseName");
            return View(chemicalOrderDetails);
        }

        // GET: PostOrders/Create
        public IActionResult CreateConsumableDetails(int orderId)
        {

            ViewData["ConsumableID"] = new SelectList(_context.Consumables, "ConsumableID", "ConsumableName");
            ViewBag.OrderID = orderId;
            return View();
        }

        // POST: PostOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConsumableDetails(ConsumableOrderDetails consumableOrderDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consumableOrderDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = consumableOrderDetails.OrderID });
            }
            ViewData["ConsumableID"] = new SelectList(_context.Consumables, "ConsumableID", "ConsumableName");
            return View(consumableOrderDetails);
        }

        public IActionResult GetVCOrderDetail(int? id)
        {
            return ViewComponent("VCOrderDetail", new { Oid = id });
        }

    }
}
