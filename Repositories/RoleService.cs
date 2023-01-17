
using RMS.Models;
using System.Threading.Tasks;

namespace RMS.Repositories
{
    public class RoleService : IRoleService
    {
        private readonly IRole _role;
        public RoleService(IRole role)
        {
            _role = role;
        }
        public async Task<ResponseStatus> CreateRole(RoleViewModel model)
        {
            var response = await _role.CreateRole(model);
            return response;
        }
        public async Task<ResponseStatus> UpdateRole(RoleViewModel model)
        {
            var response = await _role.UpdateRole(model);
            return response;
        }
        public async Task<RoleViewModel> GetRoleById(string id)
        {
            var role = await _role.GetRoleById(id);
            var roleModel = new RoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                Description = role.Description,
                CurrentStatusId = role.CurrentStatusId
            };
            return roleModel;
        }
        public async Task<List<RoleViewModel>> GetAll()
        {
            var roles = await _role.GetAll();
            return roles.Select(v => new RoleViewModel
            {
                Id = v.Id,
                RoleName = v.Name,
                Description = v.Description,
                CurrentStatusId = v.CurrentStatusId
            }).ToList();
        }
        public async Task<List<TextValuePair>> GetAllAsTextValuePair()
        {
            var roles = await _role.GetAll();
            return roles.Select(v => new TextValuePair
            {
                Id = v.Id,
                Name = v.Name,
            }).ToList();
        }
    }
}
