
using RMS.Interfaces;
using RMS.Models;

namespace RMS.Repositories
{
    public class HREmpRoasterRepo : IHREmpRoaster
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        public HREmpRoasterRepo(RmsDbContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public HREmpRoaster Create(HREmpRoaster hremproaster)
        {
            _context.HREmpRoaster.Add(hremproaster);
            _context.SaveChanges();
            return hremproaster;
        }

        public HREmpRoaster Delete(HREmpRoaster hremproaster)
        {
            _context.HREmpRoaster.Attach(hremproaster);
            _context.Entry(hremproaster).State = EntityState.Deleted;
            _context.SaveChanges();
            return hremproaster;
        }

        public HREmpRoaster Edit(HREmpRoaster hremproaster)
        {
            _context.HREmpRoaster.Attach(hremproaster);
            _context.Entry(hremproaster).State = EntityState.Modified;
            _context.SaveChanges();
            return hremproaster;
        }

        public HREmpRoaster GetItem(int id)
        {
            HREmpRoaster item = _context.HREmpRoaster.Where(u => u.HRERId == id).FirstOrDefault();
            return item;
        }

        public PaginatedList<HREmpRoaster> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10)
        {
            List<HREmpRoaster> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.HREmpRoaster.Where(n => n.HREmpDetails.HREDEName.Contains(SearchText))
                    .Include(u=> u.HREmpDetails)
                    .ToList();
            }
            else
                items = _context.HREmpRoaster.Include(u=> u.HREmpDetails).ToList();
            items = DoSort(items, SortProperty, sortOrder);
            PaginatedList<HREmpRoaster> retItems = new PaginatedList<HREmpRoaster>(items, pageIndex, pageSize);
            return retItems;
        }
        private List<HREmpRoaster> DoSort(List<HREmpRoaster> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "Name")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.HREmpDetails.HREDEName).ToList();
                else
                    items = items.OrderByDescending(n => n.HREmpDetails.HREDEName).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.IsPresent).ToList();
                else
                    items = items.OrderByDescending(d => d.IsPresent).ToList();
            }
            return items;
        }
        public bool IsDNameExists(string name)
        {
            int ct = _context.HREmpRoaster.Where(n => n.HRERId.ToString().ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsDNameExists(int id, DateTime date)
        {

            int ct = _context.HREmpRoaster.Where(n => n.HREDId == id && n.HRERDate == date).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
