using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LabProject.Models;

public partial class ConsumableRecords
{
    [Key]
    public int RecordID { get; set; }
    [Display(Name = "員工編號")]
    [ForeignKey("Employee")]
    public string? EmployeeID { get; set; }
    [Display(Name = "耗材編號")]
    [ForeignKey("Consumables")]
    public int? ConsumableID { get; set; }
    [Display(Name = "數量")]
    [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "只能填寫正整數")]
    [Required(ErrorMessage = "必填")]
    [Range(0, 10000)]
    public short Quantity { get; set; }
    [Required(ErrorMessage = "必填")]
    [Display(Name = "日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }
    [Display(Name = "備註")]
    [DataType(DataType.MultilineText)]
    [StringLength(100, ErrorMessage = "最多100個字")]
    public string? Notes { get; set; }

    public virtual Consumables? Consumable { get; set; }
    [Display(Name = "操作者")]
    public virtual Employee? Employee { get; set; }
}
