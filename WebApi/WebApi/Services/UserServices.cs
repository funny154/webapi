using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi.Models;
using WebApi.Repoitory;

namespace WebApi.Services
{
    public class UserServices
    {
        private readonly IncrudContext _context;

        public UserServices(IncrudContext context)
        {
            _context = context;
        }
        public async Task<List<UserResponse>> GetAllUser() {

            List<UserResponse> result = new List<UserResponse>();
            var user = await _context.Users.ToListAsync();
            foreach(var item in user)
            if (user != null)
            {
                UserResponse userResponse = new UserResponse();
                userResponse.Id = item.Id;
                userResponse.Name  = item.Name;
                userResponse.Phone = item.Phone;
                result.Add(userResponse);
            }
            return result;

        }
    }
}
