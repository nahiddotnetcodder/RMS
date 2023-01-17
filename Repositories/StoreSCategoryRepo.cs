
using RMS.Interfaces;
using RMS.Models;

namespace RMS.Repositories
{
    public class StoreSCategoryRepo : IStoreSCategory
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        public StoreSCategoryRepo(RmsDbContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public StoreSCategory Create(StoreSCategory storeSCategory)
        {
            _context.StoreSCategory.Add(storeSCategory);
            _context.SaveChanges();
            return storeSCategory;
        }

        public StoreSCategory Delete(StoreSCategory storeSCategory)
        {
            _context.StoreSCategory.Attach(storeSCategory);
            _context.Entry(storeSCategory).State = EntityState.Deleted;
            _context.SaveChanges();
            return storeSCategory;
        }

        public StoreSCategory Edit(StoreSCategory storeSCategory)
        {
            _context.StoreSCategory.Attach(storeSCategory);
            _context.Entry(storeSCategory).State = EntityState.Modified;
            _context.SaveChanges();
            return storeSCategory;
        }

        public StoreSCategory GetItem(int sscid)
        {
            StoreSCategory item = _context.StoreSCategory.Where(u => u.SSCId == sscid).FirstOrDefault();
            return item;
        }

        public PaginatedList<StoreSCategory> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10)
        {
            List<StoreSCategory> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.StoreSCategory.Where(n => n.SSCName.Contains(SearchText) || n.StoreCat.SCName.Contains(SearchText)).Include(u=>u.StoreCat)
                .ToList();
            }
            else
                items = _context.StoreSCategory.Include(u => u.StoreCat).ToList();
                items = DoSort(items, SortProperty, sortOrder);
                PaginatedList<StoreSCategory> retItems = new PaginatedList<StoreSCategory>(items, pageIndex, pageSize);
                 return retItems;
        }
         private List<StoreSCategory> DoSort(List<StoreSCategory> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "Name")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.SSCName).ToList();
                else
                    items = items.OrderByDescending(n => n.SSCName).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.StoreCat.SCName).ToList();
                else
                    items = items.OrderByDescending(d => d.StoreCat.SCName).ToList();
            }
            return items;
        }

        public bool IsItemExists(string name)
        {
            int ct = _context.StoreSCategory.Where(n => n.SSCName.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string name, int sscid)
        {
            int ct = _context.StoreSCategory.Where(n => n.SSCName.ToLower() == name.ToLower() && n.SSCId != sscid).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
