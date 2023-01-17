using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class HRSalaryPolicy
    {
        [Key]
        public int HRSPId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="The Name field is required")]
        [Display(Name ="Salary Policy Name")]
        public string HRSPName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Add/Deduct")]
        public ADDUC ADDUC { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Percentage/Non Percentage")]
        public PerNPer PerNPer { get; set; }

        [Required(ErrorMessage = "The Percentage field is required")]
        [Display(Name = "Percentage/Non Percentage")]
        public float? HRSPPercent { get; set; }

        [StringLength(50)]
        public string CUser { get; set; } = "Nahid";
        public DateTime CreateDate { get; set; }=DateTime.Now;
    }
}

namespace RMS.Models
{
    public enum ADDUC
    {
        Add, Deduct
    }
}

namespace RMS.Models
{
    public enum PerNPer
    {
        Percentage, NonPercentage
    }
}