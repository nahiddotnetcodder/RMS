using System.ComponentModel;

namespace RMS.Models
{
    public class ApplicationConstants
    {
        public enum UserRole
        {
            [Description("Admin")]
            Admin = 1,
            [Description("HoD")]
            HoD = 2,
            [Description("User")]
            User = 3
        }
        public enum Status
        {
            [Description("Active")]
            Active = 1,
            [Description("InActive")]
            InActive = 2
        }
    }
}
