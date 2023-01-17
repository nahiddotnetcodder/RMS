namespace RMS.Data
{
    public class ApplicationRole : IdentityRole
    {
        [Column(TypeName = "nvarchar(250)")]
        public string Description { get; set; }
        public int CurrentStatusId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
