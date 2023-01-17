
namespace RMS.Interfaces
{
    public interface IHRDesignation
    {
        PaginatedList<HRDesignation> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10); //read all
        HRDesignation GetItem(int id); // read particular item
        HRDesignation Create(HRDesignation hrdesignation);
        HRDesignation Edit(HRDesignation hrdesignation);
        HRDesignation Delete(HRDesignation hrdesignation);
        public bool IsDNameExists(string dename);
        public bool IsDNameExists(string dename, int Id);
    }
}

