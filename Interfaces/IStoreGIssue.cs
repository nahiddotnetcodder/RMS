
namespace RMS.Interfaces
{
    public interface IStoreGIssue
    {
        PaginatedList<StoreGIssue> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5); //read all
        StoreGIssue GetItem(int rdcid); // read particular item
        StoreGIssue Create(StoreGIssue storeGIssue);
        StoreGIssue Edit(StoreGIssue storeGIssue);
        StoreGIssue Delete(StoreGIssue storeGIssue);
        public bool IsItemExists(string name);
        public bool IsItemExists(string name, int sgid);
    }
}

