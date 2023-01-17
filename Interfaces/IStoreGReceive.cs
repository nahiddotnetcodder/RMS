
namespace RMS.Interfaces
{
    public interface IStoreGReceive
    {
        PaginatedList<StoreGReceive> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5); //read all
        StoreGReceive GetItem(int rdcid); // read particular item
        StoreGReceive Create(StoreGReceive storeGReceive);
        StoreGReceive Edit(StoreGReceive storeGReceive); 
        StoreGReceive Delete(StoreGReceive storeGReceive);
        public bool IsItemExists(int id);
        public bool IsItemExists(string name, int sgrid);
    }
}

