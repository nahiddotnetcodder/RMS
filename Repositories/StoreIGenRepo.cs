
using RMS.Interfaces;
using RMS.Models;
using System.Xml.Linq;

namespace RMS.Repositories
{
    public class StoreIGenRepo : IStoreIGen
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        public StoreIGenRepo(RmsDbContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public StoreIGen Create(StoreIGen storeIGen)
        {
            _context.StoreIGen.Add(storeIGen);
            _context.SaveChanges();
            return storeIGen;
        }

        public StoreIGen Delete(StoreIGen storeIGen)
        {
            _context.StoreIGen.Attach(storeIGen);
            _context.Entry(storeIGen).State = EntityState.Deleted;
            _context.SaveChanges();
            return storeIGen;
        }

        public StoreIGen Edit(StoreIGen storeIGen)
        {
            _context.StoreIGen.Attach(storeIGen);
            _context.Entry(storeIGen).State = EntityState.Modified;
            _context.SaveChanges();
            return storeIGen;
        }

        public StoreIGen GetItem(int rdcid)
        {
            StoreIGen item = _context.StoreIGen.Where(u => u.SIGId == rdcid).FirstOrDefault();
            return item;
        }

        public PaginatedList<StoreIGen> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10)
        {
            List<StoreIGen> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.StoreIGen.Where(n => n.SIGItemCode.Contains(SearchText) || n.StoreSCategory.SSCName.Contains(SearchText) )
                    .Include(u=> u.StoreSCategory)
                    .ToList();
            }
            else
            items = _context.StoreIGen.Include(u=> u.StoreCategory).ToList();
            items = _context.StoreIGen.Include(u=> u.StoreSCategory).ToList();
            items = _context.StoreIGen.Include(u=> u.StoreUnits).ToList();
            items = DoSort(items, SortProperty, sortOrder);
            PaginatedList<StoreIGen> retItems = new PaginatedList<StoreIGen>(items, pageIndex, pageSize);
            return retItems;
        }

        private List<StoreIGen> DoSort(List<StoreIGen> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "Name")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.SIGItemCode).ToList();
                else
                    items = items.OrderByDescending(n => n.SIGItemCode).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.SIGItemName).ToList();
                else
                    items = items.OrderByDescending(d => d.SIGItemName).ToList();
            }
            return items;
        }

        public bool IsItemExists(string name)
        {
            int ct = _context.StoreIGen.Where(n => n.SIGItemName.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string code, int id)
        {
            int ct = _context.StoreIGen.Where(n => n.SIGItemCode.ToLower() == code.ToLower() && n.SIGId != id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
