using System.ComponentModel.DataAnnotations;

namespace LabProject.Models
{
    public class Categories
    {
        [Key]
        [Display(Name ="類別")]
        public int CategoryID { get; set; }
        [Display(Name = "類別")]
        [StringLength(30, ErrorMessage = "最多輸入30個字")]
        public string CategoryName { get; set; } = null!;
        public virtual List<Chemicals>? Chemicals { get; set; }

    }
}
