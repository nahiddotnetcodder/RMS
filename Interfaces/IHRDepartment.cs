
namespace RMS.Interfaces
{
    public interface IHRDepartment
    {
        PaginatedList<HRDepartment> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10); //read all
        HRDepartment GetItem(int id); // read particular item
        HRDepartment Create(HRDepartment hrdepartment);
        HRDepartment Edit(HRDepartment hrdepartment);
        HRDepartment Delete(HRDepartment hrdepartment);
        public bool IsDNameExists(string dname);
        public bool IsDNameExists(string dname, int Id);
    }
}

