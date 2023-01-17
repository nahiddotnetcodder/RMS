using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class AccChartType
    {
        [Key]
        public int ACTId { get; set; }
        [StringLength(100)]
        public string ACTName { get; set; }
        [ForeignKey("AccChartClasss")]
        public int ACCId { get; set; }
        public virtual AccChartClass AccChartClasss { get; set; }
        public int ACTParent { get; set; }
        [StringLength(50)]
        public string CUser { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
