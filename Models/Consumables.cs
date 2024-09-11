using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabProject.Models;

public partial class Consumables
{
    [Key]
    [Display(Name = "編號")]
    public int ConsumableID { get; set; }
    [Display(Name = "名稱")]
    [Required(ErrorMessage = "必填")]
    [StringLength(60, ErrorMessage = "最多60個字")]
    public string ConsumableName { get; set; } = null!;
    [Display(Name = "規格")]
    [Required(ErrorMessage = "必填")]
    [StringLength(20, ErrorMessage = "最多20個字")]
    public string QuantityPerUnit { get; set; } = null!;
    [Display(Name = "數量")]
    [Required(ErrorMessage = "必填")]
    [RegularExpression(@"^\d+$", ErrorMessage = "請填寫數字")]
    public short UnitInStock { get; set; }
    [Display(Name = "安全量")]
    [RegularExpression(@"^\d+$", ErrorMessage = "請填寫數字")]
    public short? SafetyLevel { get; set; }
    [Display(Name = "效期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime? ExpireDate { get; set; }
    [Display(Name = "房號")]
    [RegularExpression(@"^\d+$", ErrorMessage = "請填寫數字")]
    [Required(ErrorMessage = "必填")]
    public short Room { get; set; }
    [Display(Name = "櫃號")]
    [StringLength(5, ErrorMessage = "請填5碼以內編號")]
    public string? Cabinet { get; set; }
    [Display(Name = "架號")]
    [StringLength(5, ErrorMessage = "請填5碼以內編號")]
    public string? Shelf { get; set; }
    [Display(Name = "儲位描述")]
    [DataType(DataType.MultilineText)]
    [StringLength(100, ErrorMessage = "最多100個字")]
    public string? Description { get; set; }
    [Display(Name = "供應商")]
    [ForeignKey("Suppliers")]
    public int? SupplierID { get; set; }
    [Display(Name = "照片")]
    public byte[]? Photo { get; set; }
    [HiddenInput]
    public string? PhotoType { get; set; }

    public virtual ICollection<ConsumableOrderDetails> ConsumableOrderDetails { get; set; } = new List<ConsumableOrderDetails>();

    public virtual ICollection<ConsumableRecords> ConsumableRecords { get; set; } = new List<ConsumableRecords>();
    [Display(Name = "供應商")]
    public virtual Supplier? Supplier { get; set; }
}
