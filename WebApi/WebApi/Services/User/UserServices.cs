using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi.Models;
using WebApi.Repoitory;

namespace WebApi.Services.User
{
    public class UserServices : IUserServices
    {
        private readonly IncrudContext _context;

        public UserServices(IncrudContext context)
        {
            _context = context;
        }
        public async Task<List<UserResponse>> GetAllUser()
        {

            List<UserResponse> result = new List<UserResponse>();
            var user = await _context.Users.ToListAsync();
            foreach (var item in user)
                if (user != null)
                {
                    UserResponse userResponse = new UserResponse();
                    userResponse.Id = item.Id;
                    userResponse.Name = item.Name;
                    userResponse.Phone = item.Phone;
                    result.Add(userResponse);
                }
            return result;

        }

        public async Task<UserResponse> GetUser(int id)
        {

            UserResponse result = new UserResponse();
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                result.Id = user.Id;
                result.Name = user.Name;
                result.Phone = user.Phone;
            }
            return result;

        }

        public async Task<UserResponse> AddUser(Models.User user)
        {

            UserResponse result = new UserResponse();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            if (user != null)
            {
                result.Id = user.Id;
                result.Name = user.Name;
                result.Phone = user.Phone;
            }
            return result;

        }

        public async Task UpadteUser(Models.User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
