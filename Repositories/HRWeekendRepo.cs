
using RMS.Interfaces;
using RMS.Models;

namespace RMS.Repositories
{
    public class HRWeekendRepo : IHRWeekend
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        public HRWeekendRepo(RmsDbContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public HRWeekend Create(HRWeekend hrweekend)
        {
            _context.HRWeekend.Add(hrweekend);
            _context.SaveChanges();
            return hrweekend;
        }

        public HRWeekend Delete(HRWeekend hrweekend)
        {
            _context.HRWeekend.Attach(hrweekend);
            _context.Entry(hrweekend).State = EntityState.Deleted;
            _context.SaveChanges();
            return hrweekend;
        }

        public HRWeekend Edit(HRWeekend hrweekend)
        {
            _context.HRWeekend.Attach(hrweekend);
            _context.Entry(hrweekend).State = EntityState.Modified;
            _context.SaveChanges();
            return hrweekend;
        }

        public HRWeekend GetItem(int id)
        {
            HRWeekend item = _context.HRWeekend.Where(u => u.HRWId == id).FirstOrDefault();
            return item;
        }

        public PaginatedList<HRWeekend> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10)
        {
            List<HRWeekend> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.HRWeekend.Where(n => n.HRWId.ToString().Contains(SearchText) )
                    .ToList();
            }
            else
                items = _context.HRWeekend.ToList();
            items = DoSort(items, SortProperty, sortOrder);
            PaginatedList<HRWeekend> retItems = new PaginatedList<HRWeekend>(items, pageIndex, pageSize);
            return retItems;
        }
        private List<HRWeekend> DoSort(List<HRWeekend> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "Name")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.Weekend).ToList();
                else
                    items = items.OrderByDescending(n => n.Weekend).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.HRWId).ToList();
                else
                    items = items.OrderByDescending(d => d.HRWId).ToList();
            }
            return items;
        }
        public bool IsDNameExists(string name)
        {
            int ct = _context.HRWeekend.Where(n => n.Weekend.ToString().ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsDNameExists(string name, int Id)
        {

            int ct = _context.HRWeekend.Where(n => n.Weekend.ToString().ToLower() == name.ToLower() && n.HRWId != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
