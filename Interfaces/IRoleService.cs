using RMS.Models;
using System.Threading.Tasks;

namespace RMS.Interfaces
{
    public interface IRoleService
    {
        Task<ResponseStatus> CreateRole(RoleViewModel model);
        Task<ResponseStatus> UpdateRole(RoleViewModel model);
        Task<RoleViewModel> GetRoleById(string id);
        Task<List<RoleViewModel>> GetAll();
        Task<List<TextValuePair>> GetAllAsTextValuePair();
    }
}
