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
    public class StoreGIssue
    {
        [Key]
        public int GIId { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime GIDate { get; set; }

        [ForeignKey("StoreIG")]
        [Required]
        [Display(Name = "Item Name")]
        public int SIGId { get; set; }
        public virtual StoreIGen StoreIG { get; set; }

        [ForeignKey("StoreCategory")]
        [Display(Name = "Category Name")]
        public int? SCId { get; set; }
        public virtual StoreCategory StoreCategory { get; set; }

        [ForeignKey("StoreSCategory")]
        [Display(Name = "Sub-Category Name")]
        public int? SSCId { get; set; }
        public virtual StoreSCategory StoreSCategory { get; set; }

        [ForeignKey("StoreUnit")]
        [Display(Name = "Unit Name")]
        public int? SUId { get; set; }
        public virtual StoreUnit StoreUnit { get; set; }


        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "The Price field is required")]
        public float GIUPrice { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "The Quantity field is required")]
        public float GIQty { get; set; }

        [Display(Name = "Total Price")]
        public int GITPrice { get; set; }

        [ForeignKey("HRDepart")]
        [Required]
        [Display(Name = "HR Department")]
        public int HRDId { get; set; }
        public virtual HRDepartment HRDepart { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(50)]
        [MaxLength(250)]
        public string GIRemarks { get; set; }


        public string CUser { get; set; } = "Nahid";

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
