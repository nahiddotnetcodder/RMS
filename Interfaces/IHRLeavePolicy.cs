
namespace RMS.Interfaces
{
    public interface IHRLeavePolicy
    {
        PaginatedList<HRLeavePolicy> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 10); //read all
        HRLeavePolicy GetItem(int id); // read particular item
        HRLeavePolicy Create(HRLeavePolicy hrleavepolicy);
        HRLeavePolicy Edit(HRLeavePolicy hrleavepolicy);
        HRLeavePolicy Delete(HRLeavePolicy hrleavepolicy);
        public bool IsDNameExists(string name);
        public bool IsDNameExists(string name, int Id);
    }
}

