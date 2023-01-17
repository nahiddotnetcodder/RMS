
using RMS.Interfaces;
using RMS.Models;

namespace RMS.Repositories
{
    public class StoreUnitRepo : IStoreUnit
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        public StoreUnitRepo(RmsDbContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public StoreUnit Create(StoreUnit storeUnit)
        {
            _context.StoreUnit.Add(storeUnit);
            _context.SaveChanges();
            return storeUnit;
        }

        public StoreUnit Delete(StoreUnit storeUnit)
        {
            _context.StoreUnit.Attach(storeUnit);
            _context.Entry(storeUnit).State = EntityState.Deleted;
            _context.SaveChanges();
            return storeUnit;
        }

        public StoreUnit Edit(StoreUnit storeUnit)
        {
            _context.StoreUnit.Attach(storeUnit);
            _context.Entry(storeUnit).State = EntityState.Modified;
            _context.SaveChanges();
            return storeUnit;
        }

        public StoreUnit GetItem(int rdcid)
        {
            StoreUnit item = _context.StoreUnit.Where(u => u.SUId ==  rdcid).FirstOrDefault();
            return item;
        }

        public PaginatedList<StoreUnit> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10)
        {
            List<StoreUnit> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.StoreUnit.Where(n => n.SUName.Contains(SearchText) || n.CUser.Contains(SearchText))
                    .ToList();
            }
            else
                items = _context.StoreUnit.ToList();
            items = DoSort(items, SortProperty, sortOrder);
            PaginatedList<StoreUnit> retItems = new PaginatedList<StoreUnit>(items, pageIndex, pageSize);
            return retItems;
        }
        private List<StoreUnit> DoSort(List<StoreUnit> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "Name")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.SUName).ToList();
                else
                    items = items.OrderByDescending(n => n.SUName).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.SUId).ToList();
                else
                    items = items.OrderByDescending(d => d.SUId).ToList();
            }
            return items;
        }

        public bool IsItemExists(string name)
        {
            int ct = _context.StoreUnit.Where(n => n.SUName.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string name, int suid)
        {
            int ct = _context.StoreUnit.Where(n => n.SUName.ToLower() == name.ToLower() && n.SUId != suid).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
