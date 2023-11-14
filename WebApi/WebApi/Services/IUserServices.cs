using WebApi.Models;

namespace WebApi.Services
{
    public interface IUserServices
    {
        public Task<List<UserResponse>> GetAllUser();
        public Task<UserResponse> GetUser(int id);
        public Task<UserResponse> AddUser(Models.User user);
        public Task UpadteUser(Models.User user);
        public Task<bool> DeleteUser(int id);
        public bool UserExists(int id);
    }
}
