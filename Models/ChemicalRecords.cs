using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LabProject.Models;

public partial class ChemicalRecords
{
    [Key]
    public int RecordID { get; set; }
    [Display(Name = "員工編號")]
    [ForeignKey("Employee")]
    public string? EmployeeID { get; set; }
    [Display(Name = "藥品編號")]
    [ForeignKey("Chemicals")]
    public int? ChemicalID { get; set; }
    [Display(Name = "數量")]
    [Required(ErrorMessage = "必填")]
    [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "請填數字")]
    public double Quantity { get; set; }
    [Required(ErrorMessage = "必填")]
    [Display(Name = "日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }
    [Display(Name = "備註")]
    [DataType(DataType.MultilineText)]
    [StringLength(100, ErrorMessage = "最多100個字")]
    public string? Notes { get; set; }

    public virtual Chemicals? Chemical { get; set; }
    [Display(Name = "操作者")]
    public virtual Employee? Employee { get; set; }
}
