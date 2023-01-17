using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class StoreGReceive
    {
        [Key]
        public int GRId { get; set; }

        [Required(ErrorMessage = "The Date feild is required")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime GRDate { get; set; }

        [ForeignKey("StoreIGens")]
        [Display(Name = "Item Name")]
        public int SIGId { get; set; }
        public virtual StoreIGen StoreIGens { get;  set; }

        [ForeignKey("StoreCategory")]
        [Display(Name = "Category Name")]
        public int? SCId { get; set; }
        public virtual StoreCategory StoreCategory{ get; set; }
        

        [ForeignKey("StoreSCategory")]
        [Display(Name = "Sub-Category Name")]
        public int? SSCId { get; set; }
        public virtual StoreSCategory StoreSCategory { get; set; }

        [ForeignKey("StoreUnit")]
        [Display(Name = "Unit Name")]
        public int? SUId { get; set; }
        public virtual StoreUnit StoreUnit { get;  set; }

        [Required(ErrorMessage = "The Price field is required")]
        [Display(Name = "Unit Price")]
        public float GRUPrice { get; set; }

        [Required(ErrorMessage = "The Quantity field is required")]
        [Display(Name = "Quantity")]
        public float GRQty { get; set; }
        
        [Display(Name = "Total Price")]
        public int GRTPrice { get; set; }

        [MaxLength(250)]
        [Display(Name = "Remarks")]
        public string GRRemarks { get; set; }

        public string CUser { get; set; } = "Nahid";

        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
