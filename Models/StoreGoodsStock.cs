using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class StoreGoodsStock
    {
        [Key]
        public int SGSId { get; set; }

        [Required]
        [ForeignKey("StoreIGens")]
        [Display(Name = "Name")]
        public int SIGId { get; set; }
        public virtual StoreIGen StoreIGens { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "This field is required")]
        public float SGSQty { get; set; }

        [Required(ErrorMessage = "The Price field is required")]
        [Display(Name = "Price")]
        public float SGSUPrice { get; set; }
    }
}
