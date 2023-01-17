
using RMS.Interfaces;
using RMS.Models;

namespace RMS.Repositories
{
    public class HRSalaryPolicyRepo : IHRSalaryPolicy
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        public HRSalaryPolicyRepo(RmsDbContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public HRSalaryPolicy Create(HRSalaryPolicy hrsalarypolicy)
        {
            _context.HRSalaryPolicy.Add(hrsalarypolicy);
            _context.SaveChanges();
            return hrsalarypolicy;
        }

        public HRSalaryPolicy Delete(HRSalaryPolicy hrsalarypolicy)
        {
            _context.HRSalaryPolicy.Attach(hrsalarypolicy);
            _context.Entry(hrsalarypolicy).State = EntityState.Deleted;
            _context.SaveChanges();
            return hrsalarypolicy;
        }

        public HRSalaryPolicy Edit(HRSalaryPolicy hrsalarypolicy)
        {
            _context.HRSalaryPolicy.Attach(hrsalarypolicy);
            _context.Entry(hrsalarypolicy).State = EntityState.Modified;
            _context.SaveChanges();
            return hrsalarypolicy;
        }

        public HRSalaryPolicy GetItem(int id)
        {
            HRSalaryPolicy item = _context.HRSalaryPolicy.Where(u => u.HRSPId == id).FirstOrDefault();
            return item;
        }

        public PaginatedList<HRSalaryPolicy> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10)
        {
            List<HRSalaryPolicy> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.HRSalaryPolicy.Where(n => n.HRSPName.Contains(SearchText) || n.CUser.Contains(SearchText))
                    .ToList();
            }
            else
                items = _context.HRSalaryPolicy.ToList();
            items = DoSort(items, SortProperty, sortOrder);
            PaginatedList<HRSalaryPolicy> retItems = new PaginatedList<HRSalaryPolicy>(items, pageIndex, pageSize);
            return retItems;
        }
        private List<HRSalaryPolicy> DoSort(List<HRSalaryPolicy> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "Name")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.HRSPName).ToList();
                else
                    items = items.OrderByDescending(n => n.HRSPName).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.ADDUC).ToList();
                else
                    items = items.OrderByDescending(d => d.ADDUC).ToList();
            }
            return items;
        }
        public bool IsDNameExists(string name)
        {
            int ct = _context.HRSalaryPolicy.Where(n => n.HRSPName.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsDNameExists(string name, int Id)
        {

            int ct = _context.HRSalaryPolicy.Where(n => n.HRSPName.ToLower() == name.ToLower() && n.HRSPId != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
