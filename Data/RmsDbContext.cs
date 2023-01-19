namespace RMS.Data
{
    public class RmsDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public RmsDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<AccBankAccounts> AccBankAccounts { get; set; }
        public virtual DbSet<AccChartClass> AccChartClass { get; set; }
        public virtual DbSet<AccChartMaster> AccChartMaster { get; set; }
        public virtual DbSet<AccChartType> AccChartType { get; set; }
        public virtual DbSet<AccGlTrans> AccGlTrans { get; set; }
        public virtual DbSet<AccJournal> AccJournal { get; set; }
       
        public virtual DbSet<HRDepartment> HRDepartment { get; set; }
        public virtual DbSet<HRDesignation> HRDesignation { get; set; }
        public virtual DbSet<HREmpAtt> HREmpAtt { get; set; }
        public virtual DbSet<HREmpDetails> HREmpDetails { get; set; } 
        public virtual DbSet<HREmpRoaster> HREmpRoaster { get; set; }
        public virtual DbSet<HRHolidays> HRHolidays { get; set; }
        public virtual DbSet<HRLeaveDetail> HRLeaveDetail { get; set; }
        public virtual DbSet<HRLeavePolicy> HRLeavePolicy { get; set; }
        public virtual DbSet<HREmpSalary> HRSalary { get; set; }
        public virtual DbSet<HRSalaryPolicy> HRSalaryPolicy { get; set; }
        public virtual DbSet<HRWeekend> HRWeekend { get; set; }
        public virtual DbSet<HRWStatus> HRWStatus { get; set; }
        public virtual DbSet<StoreDClose> StoreDClose { get; set; }
        public virtual DbSet<StoreCategory> StoreCategory { get; set; }
        public virtual DbSet<StoreGIssueMaster> StoreGIssueMasters { get; set; }
        public virtual DbSet<StoreGIssueDetails> StoreGIssueDetails { get; set; }
        public virtual DbSet<StoreGoodsStock> StoreGoodsStock { get; set; }
        public virtual DbSet<StoreGReceive> StoreGReceive { get; set; }
        public virtual DbSet<StoreIGen> StoreIGen { get; set; }
        public virtual DbSet<StoreSCategory> StoreSCategory { get; set; }
        public virtual DbSet<StoreUnit> StoreUnit { get; set; }
    }
}
