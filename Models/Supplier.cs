using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabProject.Models;

public partial class Supplier
{
    [Key]
    [Display(Name = "編號")]
    public int SupplierID { get; set; }
    [Display(Name = "公司名稱")]
    [StringLength(20, ErrorMessage = "最多20個字")]
    [Required(ErrorMessage = "必填")]
    public string CompanyName { get; set; } = null!;
    [Display(Name = "聯絡人")]
    [StringLength(20)]
    [Required(ErrorMessage = "必填")]
    public string ContactName { get; set; } = null!;
    [Display(Name = "連絡電話")]
    [StringLength(20, ErrorMessage = "最多20個字")]
    [Required(ErrorMessage = "必填")]
    public string ContactTel { get; set; } = null!;
    [Display(Name = "地址")]
    [StringLength(100)]
    public string? Address { get; set; }

    public virtual ICollection<Chemicals> Chemicals { get; set; } = new List<Chemicals>();

    public virtual ICollection<Consumables> Consumables { get; set; } = new List<Consumables>();
}
