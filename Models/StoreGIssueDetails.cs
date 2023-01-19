

namespace RMS.Models
{
    public class StoreGIssueDetails
    {
        public StoreGIssueDetails()
        {

        }
        [Key]
        public int GIDId { get; set; }

        [ForeignKey("StoreIG")]
        [Required]
        [Display(Name = "Item Name")]
        public int SIGId { get; set; }
        public virtual StoreIGen StoreIG { get; set; }

        [Display(Name = "Unit Price")]
        public float GIDUPrice { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "The Quantity field is required")]
        public float GIDQty { get; set; }

        [Display(Name = "Total Price")]
        public int GIDTPrice { get; set; }


        [ForeignKey("StoreGIssueMaster")]
        public int GIMId { get; set; }
        public virtual StoreGIssueMaster StoreGIssueMaster { get; set; }
    }
}
