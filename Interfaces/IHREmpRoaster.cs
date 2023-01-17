
namespace RMS.Interfaces
{
    public interface IHREmpRoaster
    {
        PaginatedList<HREmpRoaster> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10); //read all
        HREmpRoaster GetItem(int id); // read particular item
        HREmpRoaster Create(HREmpRoaster hremproaster);
        HREmpRoaster Edit(HREmpRoaster hremproaster);
        HREmpRoaster Delete(HREmpRoaster hremproaster);
        public bool IsDNameExists(string name);
        public bool IsDNameExists(int id, DateTime date);
    }
}

