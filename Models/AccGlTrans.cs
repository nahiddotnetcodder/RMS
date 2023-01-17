using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models
{
    public class AccGlTrans
    {
        [Key]
        public int AGTId { get; set; }
        //[ForeignKey("AJTypes")]
        public int AJType { get; set; }
        //public virtual AccJournal AJTypes { get; set; }
        [ForeignKey("AccTrNos")]
        public int AJTrNo { get; set; }
        public virtual AccJournal AccTrNos { get; set; }
        //[ForeignKey("AJTrDates")]
        public DateTime AJTrDate { get; set; }
        //public virtual AccJournal AJTrDates { get; set; }
        [StringLength(20)]
        public string AGTAccount { get; set; }
        [StringLength(200)]
        public string AGTMemo { get; set; }
        public double AGTAmount { get; set; }
    }
}