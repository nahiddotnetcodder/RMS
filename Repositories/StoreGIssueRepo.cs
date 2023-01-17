using DevExpress.Xpo;
using RMS.Interfaces;
using RMS.Models;
using System.Xml.Linq;

namespace RMS.Repositories
{
    public class StoreGIssueRepo:IStoreGIssue
    {
        private readonly RmsDbContext _context; 
        public StoreGIssueRepo(RmsDbContext context) 
        {
            _context = context;
        }

        public StoreGIssue Create(StoreGIssue storeGIssue)
        {
            _context.StoreGIssue.Add(storeGIssue);
            _context.SaveChanges();
            return storeGIssue;
        }

        public StoreGIssue Delete(StoreGIssue storeGIssue)
        {
            _context.StoreGIssue.Attach(storeGIssue);
            _context.Entry(storeGIssue).State = EntityState.Deleted;
            _context.SaveChanges();
            return storeGIssue;
        }

        public StoreGIssue Edit(StoreGIssue storeGIssue)
        {
            _context.StoreGIssue.Attach(storeGIssue);
            _context.Entry(storeGIssue).State = EntityState.Modified;
            _context.SaveChanges();
            return storeGIssue;
        }

        public StoreGIssue GetItem(int rdcid)
        {
            StoreGIssue item = _context.StoreGIssue.Where(u => u.GIId == rdcid).FirstOrDefault();
            return item;
        }

        public PaginatedList<StoreGIssue> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<StoreGIssue> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.StoreGIssue.Where(n => n.GIRemarks.Contains(SearchText) || n.CUser.Contains(SearchText))
                    .ToList();
            }
            else
            items = _context.StoreGIssue.Include(u=> u.HRDepart).ToList();
            items = _context.StoreGIssue.Include(u=> u.StoreCategory).ToList();
            items = _context.StoreGIssue.Include(u=> u.StoreSCategory).ToList();
            items = _context.StoreGIssue.Include(u=> u.StoreUnit).ToList();
            items = _context.StoreGIssue.Include(u=> u.StoreIG).ToList();
            items = DoSort(items, SortProperty, sortOrder);
            PaginatedList<StoreGIssue> retItems = new PaginatedList<StoreGIssue>(items, pageIndex, pageSize);
            return retItems;
        }

        private List<StoreGIssue> DoSort(List<StoreGIssue> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "Name")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.GIRemarks).ToList();
                else
                    items = items.OrderByDescending(n => n.GIRemarks).ToList();
            }
            return items;
        }

        public bool IsItemExists(string name)
        {
            int ct = _context.StoreGIssue.Where(n => n.GIRemarks.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string name, int sgid)
        {
            int ct = _context.StoreGIssue.Where(n => n.GIRemarks.ToLower() == name.ToLower() && n.SIGId != sgid).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
