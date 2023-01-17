
using RMS.Interfaces;
using RMS.Models;

namespace RMS.Repositories
{
    public class HRDepartmentRepo : IHRDepartment
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        public HRDepartmentRepo(RmsDbContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public HRDepartment Create(HRDepartment hrdepartment)
        {
            _context.HRDepartment.Add(hrdepartment);
            _context.SaveChanges();
            return hrdepartment;
        }

        public HRDepartment Delete(HRDepartment hrdepartment)
        {
            _context.HRDepartment.Attach(hrdepartment);
            _context.Entry(hrdepartment).State = EntityState.Deleted;
            _context.SaveChanges();
            return hrdepartment;
        }

        public HRDepartment Edit(HRDepartment hrdepartment)
        {
            _context.HRDepartment.Attach(hrdepartment);
            _context.Entry(hrdepartment).State = EntityState.Modified;
            _context.SaveChanges();
            return hrdepartment;
        }

        public HRDepartment GetItem(int id)
        {
            HRDepartment item = _context.HRDepartment.Where(u => u.HRDId == id).FirstOrDefault();
            return item;
        }

        public PaginatedList<HRDepartment> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10)
        {
            List<HRDepartment> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.HRDepartment.Where(n => n.HRDName.Contains(SearchText) || n.HRDDes.Contains(SearchText))
                    .ToList();
            }
            else
                items = _context.HRDepartment.ToList();
            items = DoSort(items, SortProperty, sortOrder);
            PaginatedList<HRDepartment> retItems = new PaginatedList<HRDepartment>(items, pageIndex, pageSize);
            return retItems;
        }
        private List<HRDepartment> DoSort(List<HRDepartment> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "Name")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.HRDName).ToList();
                else
                    items = items.OrderByDescending(n => n.HRDName).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.HRDDes).ToList();
                else
                    items = items.OrderByDescending(d => d.HRDDes).ToList();
            }
            return items;
        }
        public bool IsDNameExists(string dname)
        {
            int ct = _context.HRDepartment.Where(n => n.HRDName.ToLower() == dname.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsDNameExists(string dname, int Id)
        {

            int ct = _context.HRDepartment.Where(n => n.HRDName.ToLower() == dname.ToLower() && n.HRDId != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
