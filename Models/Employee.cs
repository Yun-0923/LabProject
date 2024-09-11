using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabProject.Models;

public partial class Employee
{
    [Key]
    [Display(Name = "員工編號")]
    [Required(ErrorMessage = "必填")]
    [RegularExpression("[A-Z][0-9]{5}", ErrorMessage = "格式錯誤")]
    [StringLength(6)]
    public string EmployeeID { get; set; } = null!;
    [Display(Name = "員工姓名")]
    [Required(ErrorMessage = "必填")]
    [StringLength(20, ErrorMessage = "最多輸入20個字")]

    public string Name { get; set; } = null!;
    [Display(Name = "密碼")]
    [Required(ErrorMessage = "請輸入密碼")]
    [DataType(DataType.Password)]
    [StringLength(12)]
    [MinLength(8, ErrorMessage = "請輸入8-12位密碼")]
    [MaxLength(12, ErrorMessage ="請輸入8-12位密碼")]
    public string password { get; set; } = null!;
    [Required(ErrorMessage = "必填")]
    [Display(Name = "身分")]
    public int Role { get; set; }
    [Display(Name = "信箱")]
    [EmailAddress(ErrorMessage ="信箱格式錯誤")]
    [Required(ErrorMessage = "必填")]
    [StringLength(40, ErrorMessage = "信箱最多輸入40個字")]

    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "必填")]
    [Display(Name = "註冊時間")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]

    public DateTime CreatedDate { get; set; }
    [Display(Name = "帳號狀態")]

    public bool IsActive { get; set; }

    public virtual ICollection<ChemicalRecords>? ChemicalRecords { get; set; } = new List<ChemicalRecords>();

    public virtual ICollection<ConsumableRecords>? ConsumableRecords { get; set; } = new List<ConsumableRecords>();

    public virtual ICollection<Orders>? Orders { get; set; } = new List<Orders>();

    public virtual ICollection<SampleRecords>? SampleRecords { get; set; } = new List<SampleRecords>();
}
