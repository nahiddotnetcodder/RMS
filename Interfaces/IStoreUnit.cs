
namespace RMS.Interfaces
{
    public interface IStoreUnit
    {
        PaginatedList<StoreUnit> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5); //read all
        StoreUnit GetItem(int rdcid); // read particular item
        StoreUnit Create(StoreUnit storeUnit);
        StoreUnit Edit(StoreUnit storeUnit);
        StoreUnit Delete(StoreUnit storeUnit);
        public bool IsItemExists(string name);
        public bool IsItemExists(string name, int suid);
    }
}

