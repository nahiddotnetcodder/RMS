using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class AccChartMaster
    {
        [Key]
        //[Column(Order = 1)]
        public int ACMId { get; set; }
        //[Key]
        //[Column(Order = 2)]
        [StringLength(20)]
        public string ACMAccCode { get; set; }
        [StringLength(200)]
        public string ACMAccName { get; set; }
        [ForeignKey("AccChartTypes")]
        public int ACTId { get; set; }
        public virtual AccChartType AccChartTypes { get; set; }
        public ACMAI ACMAI { get; set; }
        public bool IsActive { get; set; }
        [StringLength(50)]
        public string CUser { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

namespace RMS.Models
{
    public enum ACMAI
    {
        Active = 0, Inactive = 1
    }
}