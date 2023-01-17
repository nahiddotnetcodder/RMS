        using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class HREmpRoaster
    {
        [Key]
        public int HRERId { get; set; }

        [ForeignKey("HREmpDetails")]
        [Required]
        [Display(Name = "Employee Name")]
        public int HREDId { get; set; }
        public virtual HREmpDetails HREmpDetails { get; private set; }
        public virtual List<HREmpDetails> HREDEName { get; set; } = new List<HREmpDetails>();

        [Display(Name ="Date")]
        [DataType(DataType.Date)]
        public DateTime HRERDate { get; set; }=DateTime.Now;

        [Display(Name ="Is Present")]
        public bool IsPresent { get; set; }
        [StringLength(50)]
        public string CUser { get; set; } = "Nahid";
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
