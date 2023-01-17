
namespace RMS.Interfaces
{
    public interface IStoreSCategory
    {
        PaginatedList<StoreSCategory> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5); //read all
        StoreSCategory GetItem(int sscid); // read particular item
        StoreSCategory Create(StoreSCategory storeSCategory);
        StoreSCategory Edit(StoreSCategory storeSCategory);
        StoreSCategory Delete(StoreSCategory storeSCategory);
        public bool IsItemExists(string name);
        public bool IsItemExists(string name, int sscid);
    }
}

