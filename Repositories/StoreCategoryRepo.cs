
using RMS.Interfaces;
using RMS.Models;

namespace RMS.Repositories
{
    public class StoreCategoryRepo : IStoreCategory
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        public StoreCategoryRepo(RmsDbContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public StoreCategory Create(StoreCategory storeCategory)
        {
            _context.StoreCategory.Add(storeCategory);
            _context.SaveChanges();
            return storeCategory;
        }

        public StoreCategory Delete(StoreCategory storeCategory)
        {
            _context.StoreCategory.Attach(storeCategory);
            _context.Entry(storeCategory).State = EntityState.Deleted;
            _context.SaveChanges();
            return storeCategory;
        }

        public StoreCategory Edit(StoreCategory storeCategory)
        {
            _context.StoreCategory.Attach(storeCategory);
            _context.Entry(storeCategory).State = EntityState.Modified;
            _context.SaveChanges();
            return storeCategory;
        }

        public StoreCategory GetItem(int scid)
        {
            StoreCategory item = _context.StoreCategory.Where(u => u.SCId == scid).FirstOrDefault();
            return item;
        }

        public PaginatedList<StoreCategory> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10)
        {
            List<StoreCategory> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.StoreCategory.Where(n => n.SCName.Contains(SearchText) || n.CUser.Contains(SearchText))
                    .ToList();
            }
            else
                items = _context.StoreCategory.ToList();
            items = DoSort(items, SortProperty, sortOrder);
            PaginatedList<StoreCategory> retItems = new PaginatedList<StoreCategory>(items, pageIndex, pageSize);
            return retItems;
        }
         private List<StoreCategory> DoSort(List<StoreCategory> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "Name")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.SCName).ToList();
                else
                    items = items.OrderByDescending(n => n.SCName).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.SCId).ToList();
                else
                    items = items.OrderByDescending(d => d.SCId).ToList();
            }
            return items;
        }

        public bool IsItemExists(string name)
        {
            int ct = _context.StoreCategory.Where(n => n.SCName.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string name, int sscid)
        {
            int ct = _context.StoreCategory.Where(n => n.SCName.ToLower() == name.ToLower() && n.SCId != sscid).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
