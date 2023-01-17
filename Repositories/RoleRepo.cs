using RMS.Models;
using System.Threading.Tasks;

namespace RMS.Repositories
{
    public class RoleRepo : IRole
    {
        private readonly RmsDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleRepo(RmsDbContext context, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<ResponseStatus> CreateRole(RoleViewModel model)
        {
            var status = new ResponseStatus();
            if (!await _roleManager.RoleExistsAsync(model.RoleName))
            {
                var roleModel = new ApplicationRole
                {
                    Name = model.RoleName,
                    Description = model.Description,
                    CurrentStatusId = model.CurrentStatusId,
                    NormalizedName = model.RoleName
                };
                _context.ApplicationRoles.Add(roleModel);
                var result = await _context.SaveChangesAsync();
                if (result < 1)
                {
                    status.StatusCode = 0;
                    status.Message = "Role Creation Failed";
                    return status;
                }
            }
            status.StatusCode = 1;
            status.Message = "Role Created successfully";
            return status;
        }

        public async Task<ApplicationRole> GetRoleById(string id)
        {
            var role = await _context.ApplicationRoles.Where(x=> x.Id == id).FirstOrDefaultAsync();
            return role;
        }

        public async Task<ResponseStatus> UpdateRole(RoleViewModel model)
        {
            var status = new ResponseStatus();
            var role = await _context.ApplicationRoles.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
            role.Name = model.RoleName;
            role.Description = model.Description;
            role.CurrentStatusId = model.CurrentStatusId;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "Role Update Failed";
                return status;
            }
            status.StatusCode = 1;
            status.Message = "Role Updated successfully";
            return status;
        }
        public async Task<List<ApplicationRole>> GetAll()
        {
            var roles = await _context.ApplicationRoles.ToListAsync();
            return roles;
        }
    }
}
