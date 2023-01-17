using static RMS.Models.ApplicationConstants;

namespace RMS.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Current Status")]
        public int CurrentStatusId { get; set; }
        public string CurrentStatusName
        {
            get
            {
                if (CurrentStatusId <= 0)
                    return string.Empty;
                return EnumUtility.GetDescriptionFromEnumValue((Status)CurrentStatusId);
            }
        }
    }
}
