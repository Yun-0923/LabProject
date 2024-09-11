using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabProject.Models;

public partial class Samples
{
    [Key]
    [Display(Name = "編號")]
    public long SampleID { get; set; }

    [Display(Name = "名稱")]
    [Required(ErrorMessage = "必填")]
    [StringLength(60, ErrorMessage = "請勿超過60個字")]
    public string SampleName { get; set; } = null!;
    [Display(Name = "物種")]
    [StringLength(40, ErrorMessage = "最多40個字")]
    public string? Species { get; set; }
    [Display(Name = "基因型")]
    [StringLength(40, ErrorMessage = "最多40個字")]
    public string? Genotype { get; set; }
    [Display(Name = "血清型")]
    public short? Serotype { get; set; }
    [Display(Name = "數量")]
    [Required(ErrorMessage = "必填")]
    [RegularExpression(@"^\d+$", ErrorMessage = "只能填寫正整數")]
    public short Quantity { get; set; }
    [Display(Name = "冰箱編號")]
    [Required(ErrorMessage = "必填")]
    [StringLength(4, ErrorMessage = "編號最多4碼")]
    public string Refrigerator { get; set; } = null!;
    [Display(Name = "層號")]
    [Required(ErrorMessage = "必填")]
    [Range(1, 10, ErrorMessage = "請填1-10的整數")]
    public short Level { get; set; }
    [Display(Name = "架號")]
    [Required(ErrorMessage = "必填")]
    [Range(1, 10, ErrorMessage = "請填1-10的整數")]
    public short Shelf { get; set; }
    [Display(Name = "盒號")]
    [Required(ErrorMessage = "必填")]
    [Range(1, 10, ErrorMessage = "請填1-10的整數")]
    public short Box { get; set; }
    [Display(Name = "盒子名稱")]
    [Required(ErrorMessage = "必填")]
    [StringLength(40, ErrorMessage = "請勿超過40個字")]
    public string BoxName { get; set; } = null!;
    [Display(Name = "樣本類型")]
    [ForeignKey("SampleType")]
    public int TypeID { get; set; }

    public virtual ICollection<SampleRecords>? SampleRecords { get; set; } = new List<SampleRecords>();

    public virtual SampleType? SampleType { get; set; }
}
