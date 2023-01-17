
namespace RMS.Interfaces
{
    public interface IStoreCategory
    {
        PaginatedList<StoreCategory> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5); //read all
        StoreCategory GetItem(int scid); // read particular item
        StoreCategory Create(StoreCategory storeCategory);
        StoreCategory Edit(StoreCategory storeCategory);
        StoreCategory Delete(StoreCategory storeCategory);
        public bool IsItemExists(string name);
        public bool IsItemExists(string name, int scid);
    }
}

