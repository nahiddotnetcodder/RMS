
namespace RMS.Interfaces
{
    public interface IHRWStatus
    {
        PaginatedList<HRWStatus> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10); //read all
        HRWStatus GetItem(int id); // read particular item
        HRWStatus Create(HRWStatus hrstatus);
        HRWStatus Edit(HRWStatus hrstatus);
        HRWStatus Delete(HRWStatus hrstatus);
        public bool IsStatusExists(string sname);
        public bool IsStatusExists(string sname, int Id);
    }
}

