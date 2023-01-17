using DevExpress.Xpo;
using RMS.Interfaces;
using RMS.Models;
using System.Xml.Linq;

namespace RMS.Repositories
{
    public class StoreGReceiveRepo:IStoreGReceive
    {
        private readonly RmsDbContext _context;

        public StoreGReceiveRepo(RmsDbContext context)
        {
            _context = context;
        }

        public StoreGReceive Create(StoreGReceive storeGReceive)
        {
            _context.StoreGReceive.Add(storeGReceive);
            _context.SaveChanges();
            return storeGReceive;
        }

        public StoreGReceive Delete(StoreGReceive storeGReceive)
        {
            _context.StoreGReceive.Attach(storeGReceive);
            _context.Entry(storeGReceive).State = EntityState.Deleted;
            _context.SaveChanges();
            return storeGReceive; 
        }

        public StoreGReceive Edit(StoreGReceive storeGReceive)
        {
            _context.StoreGReceive.Attach(storeGReceive);
            _context.Entry(storeGReceive).State = EntityState.Modified;
            _context.SaveChanges();
            return storeGReceive;
        }

        public StoreGReceive GetItem(int rdcid)
        {
            StoreGReceive item = _context.StoreGReceive.Where(u => u.GRId == rdcid).FirstOrDefault();
            return item;
        }

        public PaginatedList<StoreGReceive> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<StoreGReceive> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.StoreGReceive.Where(n => n.GRRemarks.Contains(SearchText) )
                    .ToList();
            }
            else
                items = _context.StoreGReceive.Include(u=> u.StoreIGens).ToList();
                items = _context.StoreGReceive.Include(u=> u.StoreCategory).ToList();
                items = _context.StoreGReceive.Include(u=> u.StoreSCategory).ToList();
                items = _context.StoreGReceive.Include(u=> u.StoreUnit).ToList();
            items = DoSort(items, SortProperty, sortOrder);
            PaginatedList<StoreGReceive> retItems = new PaginatedList<StoreGReceive>(items, pageIndex, pageSize);
            return retItems;
        }

        private List<StoreGReceive> DoSort(List<StoreGReceive> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "HRDName")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.GRRemarks).ToList();
                else
                    items = items.OrderByDescending(n => n.GRRemarks).ToList();
            }

            return items;
        }

        public bool IsItemExists(int id)
        {
            int ct = _context.StoreGoodsStock.Where(n => n.SIGId == id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string name, int sgrid)
        {
            int ct = _context.StoreGReceive.Where(n => n.GRRemarks.ToLower() == name.ToLower() && n.GRId != sgrid).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
