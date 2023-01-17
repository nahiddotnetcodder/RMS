
namespace RMS.Interfaces
{
    public interface IHRWeekend
    {
        PaginatedList<HRWeekend> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10); //read all
        HRWeekend GetItem(int id); // read particular item
        HRWeekend Create(HRWeekend hrweekend);
        HRWeekend Edit(HRWeekend hrweekend);
        HRWeekend Delete(HRWeekend hrweekend);
        public bool IsDNameExists(string name);
        public bool IsDNameExists(string name, int Id);
    }
}

