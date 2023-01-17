using RMS.Models;
using System.Threading.Tasks;

namespace RMS.Interfaces
{
    public interface IUserService
    {
        Task<ResponseStatus> CreateUser(User model);
        Task<ResponseStatus> UpdateUser(UserViewModel model);
        Task<List<UserViewModel>> GetUsers();
        Task<UserViewModel> GetUserById(string id);
    }
}
