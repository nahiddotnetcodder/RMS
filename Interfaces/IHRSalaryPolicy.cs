
namespace RMS.Interfaces
{
    public interface IHRSalaryPolicy
    {
        PaginatedList<HRSalaryPolicy> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10); //read all
        HRSalaryPolicy GetItem(int id); // read particular item
        HRSalaryPolicy Create(HRSalaryPolicy hrsalarypolicy);
        HRSalaryPolicy Edit(HRSalaryPolicy hrsalarypolicy);
        HRSalaryPolicy Delete(HRSalaryPolicy hrsalarypolicy);
        public bool IsDNameExists(string name);
        public bool IsDNameExists(string name, int Id);
    }
}

