using LabProject.Models;
using LabProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabProject.Views.ViewComponents
{
    public class VCOrderDetail : ViewComponent
    {
        private readonly dbLabContext _context;

        public VCOrderDetail(dbLabContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int Oid)
        {
            var chemicalOrderDetails = await _context.ChemicalOrderDetails
                .Include(c => c.Chemical)
                    .ThenInclude(c => c.Supplier)
                .Include(c => c.Order)
                .Where(m => m.OrderID == Oid).ToListAsync();

            var consumableOrderDetails = await _context.ConsumableOrderDetails
                .Include(c => c.Consumable)
                    .ThenInclude(c => c.Supplier)
                .Include(c => c.Order)
                .Where(m => m.OrderID == Oid).ToListAsync();

            var viewModel = new VMorderDetail
            {
                ChemicalOrderDetails = chemicalOrderDetails,
                ConsumableOrderDetails = consumableOrderDetails
            };

            return View(viewModel);
        }

    }
}
