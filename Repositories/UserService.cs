
using RMS.Models;
using System.Threading.Tasks;

namespace RMS.Repositories
{
    public class UserService : IUserService
    {

        private readonly IUser _user;
        public UserService(IUser user)
        {
            _user = user;
        }

        public async Task<ResponseStatus> CreateUser(User model)
        {
            var response = await _user.CreateUser(model);
            return response;
        }
        public async Task<ResponseStatus> UpdateUser(UserViewModel model)
        {
            var response = await _user.UpdateUser(model);
            return response;
        }
        public async Task<UserViewModel> GetUserById(string id)
        {
            var users = await _user.GetUserById(id);
            return users;
        }
        public async Task<List<UserViewModel>> GetUsers()
        {
            var users = await _user.GetUsers();
            return users;
        }
    }
}
