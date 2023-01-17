using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class HRHolidays
    {
        [Key]
        public int HRHId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="The Name feild is required")]
        [MaxLength(50)]
        [Display(Name ="Holiday Name")]
        public string HRHName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime HRHStartDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime HRHEndDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string CUser { get; set; } = "Nahid";
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
