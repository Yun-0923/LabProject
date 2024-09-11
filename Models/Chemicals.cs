using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabProject.Models;

public partial class Chemicals
{
    [Key]
    [Display(Name = "編號")]
    public int ChemicalID { get; set; }
    [Display(Name = "藥品名")]
    [Required(ErrorMessage = "必填")]
    [StringLength(60, ErrorMessage = "最多輸入60個字")]
    public string ChineseName { get; set; } = null!;
    [Display(Name = "英文名")]
    [StringLength(60, ErrorMessage = "最多輸入60個字")]
    [Required(ErrorMessage = "必填")]
    public string EnglishName { get; set; } = null!;
    [Display(Name = "CAS No.")]
    [StringLength(20, ErrorMessage = "最多輸入20個字")]
    public string? CASNo { get; set; }

    [Display(Name = "濃度")]
    [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "請填數字")]
    public double? Concentration { get; set; }
    [Display(Name = "單位")]
    [Required(ErrorMessage = "必填")]
    public bool Unit { get; set; }
    [Display(Name = "數量")]
    [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "請填數字")]
    [Required(ErrorMessage = "必填")]
    public double Stock { get; set; }
    [Display(Name = "安全量")]
    [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "請填數字")]
    public double? SafetyLevel { get; set; }
    [Display(Name = "保存條件")]
    [Required(ErrorMessage = "必填")]
    public bool StorageCondition { get; set; }
    [Display(Name = "儲櫃名稱")]
    [StringLength(10, ErrorMessage = "最多輸入10個字")]
    [Required(ErrorMessage = "必填")]
    public string CabinetName { get; set; } = null!;
    [Display(Name = "儲櫃號碼")]
    public short? CabinetNumber { get; set; }
    [Display(Name = "供應商編號")]
    public int? SupplierID { get; set; }

    public int? CategoryID { get; set; }

    public virtual ICollection<ChemicalOrderDetails> ChemicalOrderDetails { get; set; } = new List<ChemicalOrderDetails>();

    public virtual ICollection<ChemicalRecords> ChemicalRecords { get; set; } = new List<ChemicalRecords>();
    [Display(Name = "供應商")]
    public virtual Supplier? Supplier { get; set; }

    public virtual Categories? Categories { get; set; }
}
