
namespace RMS.Repositories
{
    public class HRDesignationRepo : IHRDesignation
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        public HRDesignationRepo(RmsDbContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public HRDesignation Create(HRDesignation hrdesignation)
        {
            _context.HRDesignation.Add(hrdesignation);
            _context.SaveChanges();
            return hrdesignation;
        }
        public HRDesignation Delete(HRDesignation hrdesignation)
        {
            _context.HRDesignation.Attach(hrdesignation);
            _context.Entry(hrdesignation).State = EntityState.Deleted;
            _context.SaveChanges();
            return hrdesignation;
        }
        public HRDesignation Edit(HRDesignation hrdesignation)
        {
            _context.HRDesignation.Attach(hrdesignation);
            _context.Entry(hrdesignation).State = EntityState.Modified;
            _context.SaveChanges();
            return hrdesignation;
        }
        private List<HRDesignation> DoSort(List<HRDesignation> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "HRDeName")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.HRDeName).ToList();
                else
                    items = items.OrderByDescending(n => n.HRDeName).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.HRDeDes).ToList();
                else
                    items = items.OrderByDescending(d => d.HRDeDes).ToList();
            }
            return items;
        }
        public PaginatedList<HRDesignation> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10)
        {
            List<HRDesignation> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.HRDesignation.Where(n => n.HRDeName.Contains(SearchText) || n.HRDeDes.Contains(SearchText))
                    .ToList();
            }
            else
                items = _context.HRDesignation.ToList();
                items = DoSort(items, SortProperty, sortOrder);
                PaginatedList<HRDesignation> retItems = new PaginatedList<HRDesignation>(items, pageIndex, pageSize);
                return retItems;
        }
        public HRDesignation GetItem(int id)
        {
            HRDesignation item = _context.HRDesignation.Where(u => u.HRDeId == id).FirstOrDefault();
            return item;
        }
        public bool IsDNameExists(string dename)
        {
            int ct = _context.HRDesignation.Where(n => n.HRDeName.ToLower() == dename.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        public bool IsDNameExists(string dename, int Id)
        {
            int ct = _context.HRDesignation.Where(n => n.HRDeName.ToLower() == dename.ToLower() && n.HRDeId != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
