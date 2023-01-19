using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RMS.Models
{
    public class StoreGIssueMaster
    {
        [Key]
        public int GIMId { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime GIMDate { get; set; }

        [ForeignKey("HRDepart")]
        [Required]
        [Display(Name = "Issue In Department")]
        public int HRDId { get; set; }
        public virtual HRDepartment HRDepart { get; set; }


        [Display(Name = "Remarks")]
        [StringLength(50)]
        [MaxLength(250)]
        public string GIMRemarks { get; set; }

        public string CUser { get; set; } = "Nahid";
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public virtual List<StoreGIssueDetails> StoreGIssueDetails { get; set; }= new List<StoreGIssueDetails>();

    }
}
