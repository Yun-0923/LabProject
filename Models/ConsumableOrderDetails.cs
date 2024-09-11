using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LabProject.Models;

public partial class ConsumableOrderDetails
{
    [Display(Name = "訂單編號")]
    [ForeignKey("Orders")]
    [Key]
    public int OrderID { get; set; }
    [Display(Name = "耗材編號")]
    [ForeignKey("Consumables")]
    [Key]
    public int ConsumableID { get; set; }
    [Display(Name = "數量")]
    [RegularExpression(@"^\d+$", ErrorMessage = "只能填寫數字")]
    [Required(ErrorMessage = "必填")]
    public short Quantity { get; set; }

    public virtual Consumables? Consumable { get; set; } = null!;

    public virtual Orders? Order { get; set; } = null!;
}
