using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabProject.Models;

public partial class ChemicalOrderDetails
{
    [Display(Name = "訂單編號")]
    [ForeignKey("Orders")]
    [Key]
    public int OrderID { get; set; }
    [Display(Name = "化學品編號")]
    [ForeignKey("Chemicals")]
    [Key]
    public int ChemicalID { get; set; }
    [Display(Name = "數量")]
    [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "請填數字")]
    [Required(ErrorMessage = "必填")]
    public double Quantity { get; set; }

    public virtual Chemicals? Chemical { get; set; } = null!;

    public virtual Orders? Order { get; set; } = null!;
}
