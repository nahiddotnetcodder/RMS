
using RMS.Interfaces;
using RMS.Models;

namespace RMS.Repositories
{
    public class HRHolidaysRepo : IHRHolidays
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        public HRHolidaysRepo(RmsDbContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public HRHolidays Create(HRHolidays hrholidays) 
        {
            _context.HRHolidays.Add(hrholidays);
            _context.SaveChanges();
            return hrholidays;
        }

        public HRHolidays Delete(HRHolidays hrholidays)
        {
            _context.HRHolidays.Attach(hrholidays);
            _context.Entry(hrholidays).State = EntityState.Deleted;
            _context.SaveChanges();
            return hrholidays;
        }

        public HRHolidays Edit(HRHolidays hrholidays)
        {
            _context.HRHolidays.Attach(hrholidays);
            _context.Entry(hrholidays).State = EntityState.Modified;
            _context.SaveChanges();
            return hrholidays;
        }

        public HRHolidays GetItem(int id)
        {
            HRHolidays item = _context.HRHolidays.Where(u => u.HRHId == id).FirstOrDefault();
            return item;
        }

        public PaginatedList<HRHolidays> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10)
        {
            List<HRHolidays> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.HRHolidays.Where(n => n.HRHName.Contains(SearchText) || n.CUser.Contains(SearchText))
                    .ToList();
            }
            else
                items = _context.HRHolidays.ToList();
            items = DoSort(items, SortProperty, sortOrder);
            PaginatedList<HRHolidays> retItems = new PaginatedList<HRHolidays>(items, pageIndex, pageSize);
            return retItems;
        }
        private List<HRHolidays> DoSort(List<HRHolidays> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "Name")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.HRHName).ToList();
                else
                    items = items.OrderByDescending(n => n.HRHName).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.HRHStartDate).ToList();
                else
                    items = items.OrderByDescending(d => d.CUser).ToList();
            }
            return items;
        }
        public bool IsDNameExists(string name)
        {
            int ct = _context.HRHolidays.Where(n => n.HRHName.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsDNameExists(string name, int Id)
        {

            int ct = _context.HRHolidays.Where(n => n.HRHName.ToLower() == name.ToLower() && n.HRHId != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
