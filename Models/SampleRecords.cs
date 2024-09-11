using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabProject.Models;

public partial class SampleRecords
{
    [Key]
    [Display(Name = "編號")]
    public int RecordID { get; set; }
    [Display(Name = "員工編號")]
    [ForeignKey("Employee")]
    public string? EmployeeID { get; set; }
    [Display(Name = "實驗株名稱")]
    [ForeignKey("Samples")]
    public long? SampleID { get; set; }
    [Display(Name = "類型")]
    [Required(ErrorMessage = "請選擇交易類型")]
    public bool TransactionType { get; set; }
    [Display(Name = "數量")]
    [Required(ErrorMessage = "必填")]
    [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "只能填寫正整數")]
    public short Quantity { get; set; }
    [Required(ErrorMessage = "必填")]
    [Display(Name = "日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }
    [Display(Name = "備註")]
    [DataType(DataType.MultilineText)]
    [StringLength(100, ErrorMessage = "最多100個字")]
    public string? Notes { get; set; }
    [Display(Name = "操作者")]

    public virtual Employee? Employee { get; set; }
    [Display(Name = "名稱")]

    public virtual Samples? Sample { get; set; }
}
