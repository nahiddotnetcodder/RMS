using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class AccJournal
    {
        [Key]
        public int AJId { get; set; }
        //[Key]
        public int AJType { get; set; }
        //[Key]
        public int AJTrNo { get; set; }
        //[Key]
        public DateTime AJTrDate { get; set; }
        [StringLength(50)]
        public string AJRef { get; set; }
        [StringLength(50)]
        public string AJSoRef { get; set; }
        public DateTime AJEDate { get; set; }
        public DateTime AJDDate { get; set; }
        public double AJAmount { get; set; }
        [StringLength(250)]
        public string AJMemo { get; set; }
        [StringLength(50)]
        public string CUser { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<AccGlTrans> AccGlTrans { get; set; }

    }
}
