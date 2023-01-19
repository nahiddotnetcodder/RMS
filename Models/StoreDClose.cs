using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class StoreDClose
    {
        [Key]
        public int SDCId { get; set; }

        [Required]
        [DisplayName("Current Date:")]
        [DataType(DataType.Date)]
        public DateTime SDCDate { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Last Day Closed User:")]
        public string CUser { get; set; }


    }
}
