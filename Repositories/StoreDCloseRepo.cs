


using RMS.Models;

namespace RMS.Repositories
{
    public class StoreDCloseRepo : IStoreDClose
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        public StoreDCloseRepo(RmsDbContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public StoreDClose Create(StoreDClose resdclose)
        {
            _context.StoreDClose.Add(resdclose);
            _context.SaveChanges();
            return resdclose;
        }
        public StoreDClose Edit(StoreDClose resdclose)
        {
            _context.StoreDClose.Attach(resdclose);
            _context.Entry(resdclose).State = EntityState.Modified;
            _context.SaveChanges();
            return resdclose;
        }
        private List<StoreDClose> DoSort(List<StoreDClose> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "CUser")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.CUser).ToList();
                else
                    items = items.OrderByDescending(n => n.CUser).ToList();
            }
            return items;
        }
        public PaginatedList<StoreDClose> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<StoreDClose> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.StoreDClose.Where(n => n.CUser.Contains(SearchText))
                    .ToList();
            }
            else
                items = _context.StoreDClose.ToList();
                items = DoSort(items, SortProperty, sortOrder);
                PaginatedList<StoreDClose> retItems = new PaginatedList<StoreDClose>(items, pageIndex, pageSize);
                return retItems;
        }
        public StoreDClose GetItem(int rdcid)
        {
            StoreDClose item = _context.StoreDClose.Where(u => u.SDCId == rdcid).FirstOrDefault();
            return item;
        }
    }
}
