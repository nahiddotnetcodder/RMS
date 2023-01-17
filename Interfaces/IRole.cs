using RMS.Models;
using System.Threading.Tasks;

namespace RMS.Interfaces
{
    public interface IRole
    {
        Task<ResponseStatus> CreateRole(RoleViewModel model);
        Task<ResponseStatus> UpdateRole(RoleViewModel model);
        Task<ApplicationRole> GetRoleById(string id);
        Task<List<ApplicationRole>> GetAll();
    }
}

