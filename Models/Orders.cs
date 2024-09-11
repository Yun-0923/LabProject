using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabProject.Models;

public partial class Orders
{
    [Key]
    [Display(Name = "訂單編號")]
    public int OrderID { get; set; }
    [Display(Name = "員工編號")]
    [ForeignKey("Employee")]
    public string? EmployeeID { get; set; }
    [Display(Name = "訂貨日期")]
    [Required(ErrorMessage = "必填")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime OrderDate { get; set; }
    [Display(Name = "送貨日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime? DeliveryDate { get; set; }
    [Display(Name = "狀態")]
    [Required(ErrorMessage = "必填")]
    public bool Status { get; set; }
    [Display(Name = "備註")]
    [DataType(DataType.MultilineText)]
    public string? Notes { get; set; }

    public virtual ICollection<ChemicalOrderDetails> ChemicalOrderDetails { get; set; } = new List<ChemicalOrderDetails>();

    public virtual ICollection<ConsumableOrderDetails> ConsumableOrderDetails { get; set; } = new List<ConsumableOrderDetails>();
    [Display(Name = "經手員工")]
    public virtual Employee? Employee { get; set; }
}
