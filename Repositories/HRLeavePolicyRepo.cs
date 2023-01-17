
using RMS.Interfaces;
using RMS.Models;

namespace RMS.Repositories
{
    public class HRLeavePolicyRepo : IHRLeavePolicy
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        public HRLeavePolicyRepo(RmsDbContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public HRLeavePolicy Create(HRLeavePolicy hrleavepolicy)
        {
            _context.HRLeavePolicy.Add(hrleavepolicy);
            _context.SaveChanges();
            return hrleavepolicy;
        }

        public HRLeavePolicy Delete(HRLeavePolicy hrleavepolicy)
        {
            _context.HRLeavePolicy.Attach(hrleavepolicy);
            _context.Entry(hrleavepolicy).State = EntityState.Deleted;
            _context.SaveChanges();
            return hrleavepolicy;
        }

        public HRLeavePolicy Edit(HRLeavePolicy hrleavepolicy)
        {
            _context.HRLeavePolicy.Attach(hrleavepolicy);
            _context.Entry(hrleavepolicy).State = EntityState.Modified;
            _context.SaveChanges();
            return hrleavepolicy;
        }

        public HRLeavePolicy GetItem(int id)
        {
            HRLeavePolicy item = _context.HRLeavePolicy.Where(u => u.HRLPId == id).FirstOrDefault();
            return item;
        }

        public PaginatedList<HRLeavePolicy> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10)
        {
            List<HRLeavePolicy> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.HRLeavePolicy.Where(n => n.HRLPName.Contains(SearchText) || n.CUser.Contains(SearchText))
                    .ToList();
            }
            else
                items = _context.HRLeavePolicy.ToList();
            items = DoSort(items, SortProperty, sortOrder);
            PaginatedList<HRLeavePolicy> retItems = new PaginatedList<HRLeavePolicy>(items, pageIndex, pageSize);
            return retItems;
        }
        private List<HRLeavePolicy> DoSort(List<HRLeavePolicy> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "Name")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.HRLPName).ToList();
                else
                    items = items.OrderByDescending(n => n.HRLPName).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.CUser).ToList();
                else
                    items = items.OrderByDescending(d => d.CUser).ToList();
            }
            return items;
        }
        public bool IsDNameExists(string name)
        {
            int ct = _context.HRLeavePolicy.Where(n => n.HRLPName.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsDNameExists(string name, int Id)
        {

            int ct = _context.HRLeavePolicy.Where(n => n.HRLPName.ToLower() == name.ToLower() && n.HRLPId != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
