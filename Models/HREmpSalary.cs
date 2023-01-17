using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class HREmpSalary
    {
        [Key]
        public int HRSId { get; set; }

        [Required(ErrorMessage ="This feild is requied")]
        [Display(Name ="Salary Year")]
        public HRSYear HRSYear { get; set; }

        [Required(ErrorMessage = "This feild is requied")]
        [Display(Name = "Salary Month")]
        public HRSMonth HRSMonth { get; set; }

        [ForeignKey("HREmpDetails")]
        [Display(Name ="Employee Name")]
        public int HREDId { get; set; }
        public virtual HREmpDetails HREmpDetails { get; private set; }
        public virtual List<HREmpDetails> HREDEName { get; set; } = new List<HREmpDetails>();

        [Display(Name ="Basic Salary")]
        [Required(ErrorMessage = "The Basic feild is requied")]
        public double HRSBasic { get; set; }

        [Display(Name = "Total Salary")]
        [Required(ErrorMessage = "The Total feild is requied")]
        public double HRSGTotal { get; set; }

        [StringLength(50)]
        public string CUser { get; set; } = "Nahid";
        public DateTime CreateDate { get; set; }=DateTime.Now;
    }
}

namespace RMS.Models
{
    public enum HRSYear
    {
        [Description("2023")] TwoThree,
        [Description("2024")] TwoFour,
        [Description("2025")] TwoFive,
        [Description("2026")] TwoSix,
        [Description("2027")] TwoSeven,
        [Description("2028")] TwoEight,
        [Description("2029")] TwoNine,
        [Description("2030")] ThreeZero,
        [Description("2031")] ThreeOne,
        [Description("2032")] ThreeTwo
    }
}

namespace RMS.Models
{
    public enum HRSMonth
    {
        January, February, March, April, May, June, July, August, September, October, November, December
    }
}