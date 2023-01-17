
using RMS.Interfaces;
using RMS.Models;

namespace RMS.Repositories
{
    public class HREmpSalaryRepo : IHREmpSalary
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        public HREmpSalaryRepo(RmsDbContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public HREmpSalary Create(HREmpSalary hrsalary)
        {
            _context.HRSalary.Add(hrsalary);
            _context.SaveChanges();
            return hrsalary;
        }

        public HREmpSalary Delete(HREmpSalary hrsalary)
        {
            _context.HRSalary.Attach(hrsalary);
            _context.Entry(hrsalary).State = EntityState.Deleted;
            _context.SaveChanges();
            return hrsalary;
        }

        public HREmpSalary Edit(HREmpSalary hrsalary)
        {
            _context.HRSalary.Attach(hrsalary);
            _context.Entry(hrsalary).State = EntityState.Modified;
            _context.SaveChanges();
            return hrsalary;
        }

        public HREmpSalary GetItem(int id)
        {
            HREmpSalary item = _context.HRSalary.Where(u => u.HRSId == id).FirstOrDefault();
            return item;
        }

        public PaginatedList<HREmpSalary> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10)
        {
            List<HREmpSalary> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.HRSalary.Where(n => n.HREmpDetails.HREDEName.Contains(SearchText) )
                    .Include(u=> u.HREmpDetails)
                    .ToList();
            }
            else
                items = _context.HRSalary.Include(u=>u.HREmpDetails).ToList();
            items = DoSort(items, SortProperty, sortOrder);
            PaginatedList<HREmpSalary> retItems = new PaginatedList<HREmpSalary>(items, pageIndex, pageSize);
            return retItems;
        }
        private List<HREmpSalary> DoSort(List<HREmpSalary> items, string SortProperty, SortOrder sortOrder)
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
                    items = items.OrderBy(d => d.HRSYear).ToList();
                else
                    items = items.OrderByDescending(d => d.HRSYear).ToList();
            }
            return items;
        }
        public bool IsDNameExists(int id)
        {
            int ct = _context.HRSalary.Where(n => n.HREDId == id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsDNameExists(string name, int Id)
        {

            int ct = _context.HRSalary.Where(n => n.HREmpDetails.HREDEName.ToLower() == name.ToLower() && n.HREmpDetails.HREDId != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
