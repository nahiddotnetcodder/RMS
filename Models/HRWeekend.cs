using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class HRWeekend
    {
        [Key]
        public int HRWId { get; set; }
        public Weekdays Weekend { get; set; }

        [StringLength(50)] 
        public string CUser { get; set; } = "Nahid";
        public DateTime CreateDate { get; set; }=DateTime.Now;
    }
}

namespace RMS.Models
{
    public enum Weekdays
    {
        Friday, Saturday, Sunday, Monday, Tuesday, Wednesday, Thursday 
    }
}