using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class StoreSCategory
    {
        [Key]
        public int SSCId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [ForeignKey("StoreCat")]
        [Display(Name = "Category Name")]
        public int SCId { get; set; }
        public virtual StoreCategory StoreCat { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Sub-Category Name")]
        [MaxLength(50)]
        [StringLength(50)]
        public string SSCName { get; set; }

        public string CUser { get; set; } = "Nahid";

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
