using LabProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Diagnostics;

namespace LabProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly dbLabContext _context;

        public HomeController(ILogger<HomeController> logger, dbLabContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Role"] = new SelectList(_context.Employee, "Role", "Role");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Employee employee)
        {
            if (employee == null)
            {
                return View();
            }
            var result = await _context.Employee.Where(m => m.EmployeeID == employee.EmployeeID && m.password == employee.password).FirstOrDefaultAsync();

            if (result == null)
            {
                ViewData["Error"] = "帳號密碼錯誤!!";
                return View("Index");

            }
            if(result.IsActive == false)
            {
                ViewData["False"] = "該帳號已停用";
                return View("Index");

            }
            else
            {
                HttpContext.Session.SetString("Manager", JsonConvert.SerializeObject(new { employee.EmployeeID, employee.password }));

                // 將角色存入 Session
                HttpContext.Session.SetString("UserRole", JsonConvert.SerializeObject(result.Role));

                // 根據不同角色執行不同操作
                switch (result.Role)
                {
                    case 0:
                        return RedirectToAction("Index", "Samples");
                    case 1:
                        return RedirectToAction("Index", "PostSamples");
                    case 2:
                        return RedirectToAction("Index", "PostOrders");
                    default:
                        return RedirectToAction("Index", "PostSamples");
                }
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Manager");
            HttpContext.Session.Remove("UserRole");
            return RedirectToAction("Index", "Home");
        }
    }
}
