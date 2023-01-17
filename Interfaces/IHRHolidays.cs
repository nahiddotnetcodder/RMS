
namespace RMS.Interfaces
{
    public interface IHRHolidays
    {
        PaginatedList<HRHolidays> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10); //read all
        HRHolidays GetItem(int id); // read particular item
        HRHolidays Create(HRHolidays hrholidays);
        HRHolidays Edit(HRHolidays hrholidays);
        HRHolidays Delete(HRHolidays hrholidays);
        public bool IsDNameExists(string name);
        public bool IsDNameExists(string name, int Id);
    }
}

