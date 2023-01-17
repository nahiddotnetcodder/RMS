
namespace RMS.Repositories
{
    public class HRWStatusRepo : IHRWStatus
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        public HRWStatusRepo(RmsDbContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public HRWStatus Create(HRWStatus hrstatus)
        {
            _context.HRWStatus.Add(hrstatus);
            _context.SaveChanges();
            return hrstatus;
        }
        public HRWStatus Delete(HRWStatus hrstatus)
        {
            _context.HRWStatus.Attach(hrstatus);
            _context.Entry(hrstatus).State = EntityState.Deleted;
            _context.SaveChanges();
            return hrstatus;
        }
        public HRWStatus Edit(HRWStatus hrstatus)
        {
            _context.HRWStatus.Attach(hrstatus);
            _context.Entry(hrstatus).State = EntityState.Modified;
            _context.SaveChanges();
            return hrstatus;
        }
        private List<HRWStatus> DoSort(List<HRWStatus> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "HRDName")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.HRWSName).ToList();
                else
                    items = items.OrderByDescending(n => n.HRWSName).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.HRWSDes).ToList();
                else
                    items = items.OrderByDescending(d => d.HRWSDes).ToList();
            }
            return items;
        }
        public PaginatedList<HRWStatus> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10)
        {
            List<HRWStatus> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.HRWStatus.Where(n => n.HRWSName.Contains(SearchText) || n.HRWSDes.Contains(SearchText))
                    .ToList();
            }
            else
                items = _context.HRWStatus.ToList();
                items = DoSort(items, SortProperty, sortOrder);
                PaginatedList<HRWStatus> retItems = new PaginatedList<HRWStatus>(items, pageIndex, pageSize);
                return retItems;
        }
        public HRWStatus GetItem(int id)
        {
            HRWStatus item = _context.HRWStatus.Where(u => u.HRWSId == id).FirstOrDefault();
            return item;
        }
        public bool IsStatusExists(string sname)
        {
            int ct = _context.HRWStatus.Where(n => n.HRWSName.ToLower() == sname.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsStatusExists(string sname, int Id)
        {
            int ct = _context.HRWStatus.Where(n => n.HRWSName.ToLower() == sname.ToLower() && n.HRWSId != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
