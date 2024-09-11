using System.ComponentModel.DataAnnotations;

namespace LabProject.Models
{
    public class SampleType
    {
        [Key]
        [Display(Name = "類型")]
        public int TypeID { get; set; }
        [Display(Name = "名稱")]
        [StringLength(10, ErrorMessage = "最多輸入10個字")]
        public string TypeName { get; set; } = null!;
        public virtual List<Samples>? Samples { get; set; }
    }
}
