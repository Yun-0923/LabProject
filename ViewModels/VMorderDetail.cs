using LabProject.Models;

namespace LabProject.ViewModels
{
    public class VMorderDetail
    {
        public List<ChemicalOrderDetails>? ChemicalOrderDetails { get; set; }
        public List<ConsumableOrderDetails>? ConsumableOrderDetails { get; set; }
    }
}
