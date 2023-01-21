
namespace RMS.Interfaces
{
    public interface IStoreGIssueMaster
    {
        PaginatedList<StoreGIssueMaster> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5); //read all
        StoreGIssueMaster GetItem(int rdcid); // read particular item
        StoreGIssueMaster Create(StoreGIssueMaster storeGIssue);
        StoreGIssueMaster Edit(StoreGIssueMaster storeGIssue);
        StoreGIssueMaster Delete(StoreGIssueMaster storeGIssue);
        public bool IsItemExists(string name);
        public bool IsItemExists(string name, int sgid);

        public DateTime GetDCDate();
    }
}

