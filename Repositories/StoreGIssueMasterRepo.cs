using DevExpress.Xpo;
using RMS.Interfaces;
using RMS.Models;
using System.Xml.Linq;

namespace RMS.Repositories
{
    public class StoreGIssueMasterRepo:IStoreGIssueMaster
    {
        private readonly RmsDbContext _context; 
        public StoreGIssueMasterRepo(RmsDbContext context) 
        {
            _context = context;
        }

        public StoreGIssueMaster Create(StoreGIssueMaster storeGIssue)
        {
            _context.StoreGIssueMasters.Add(storeGIssue);
            _context.SaveChanges();
            return storeGIssue;
        }

        public StoreGIssueMaster Delete(StoreGIssueMaster storeGIssue)
        {
            _context.StoreGIssueMasters.Attach(storeGIssue);
            _context.Entry(storeGIssue).State = EntityState.Deleted;
            _context.SaveChanges();
            return storeGIssue;
        }

        public StoreGIssueMaster Edit(StoreGIssueMaster storeGIssue)
        {
            _context.StoreGIssueMasters.Attach(storeGIssue);
            _context.Entry(storeGIssue).State = EntityState.Modified;
            _context.SaveChanges();
            return storeGIssue;
        }

        public StoreGIssueMaster GetItem(int rdcid)
        {
            StoreGIssueMaster item = _context.StoreGIssueMasters.Include(e=> e.StoreGIssueDetails).Where(u => u.GIMId == rdcid).FirstOrDefault();
            return item;
        }

        public PaginatedList<StoreGIssueMaster> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<StoreGIssueMaster> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.StoreGIssueMasters.Where(n => n.GIMRemarks.Contains(SearchText) || n.CUser.Contains(SearchText))
                    .ToList();
            }
            else
            items = _context.StoreGIssueMasters.Include(u=> u.HRDepart).ToList();
            items = DoSort(items, SortProperty, sortOrder);
            PaginatedList<StoreGIssueMaster> retItems = new PaginatedList<StoreGIssueMaster>(items, pageIndex, pageSize);
            return retItems;
        }

        private List<StoreGIssueMaster> DoSort(List<StoreGIssueMaster> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "Name")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.HRDepart.HRDId).ToList();
                else
                    items = items.OrderByDescending(n => n.HRDepart.HRDId).ToList();
            }
            return items;
        }

        public bool IsItemExists(string name)
        {
            int ct = _context.StoreGIssueMasters.Where(n => n.GIMRemarks.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string name, int sgid)
        {
            int ct = _context.StoreGIssueMasters.Where(n => n.GIMRemarks.ToLower() == name.ToLower() && n.GIMId != sgid).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
