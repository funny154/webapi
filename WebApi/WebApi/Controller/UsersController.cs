using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Repoitory;
using WebApi.Services;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserServices _UserServices;

        public UsersController(UserServices UserServices)
        {
            _UserServices = UserServices;
        }

        // GET: api/Users
        /// <summary>
        /// 取得User資訊
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get / Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "phone": 091234567
        ///     }
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            return StatusCode(StatusCodes.Status200OK, await _UserServices.GetAllUser());
        }

        // GET: api/Users/5
        /// <summary>
        /// 取得User資訊
        /// </summary>
        /// <param name="id">User id</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get / Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "phone": 091234567
        ///     }
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(int id)
        {
            return await _UserServices.GetUser(id);
        }

        // PUT: api/Users/5
        /// <summary>
        /// 修改 User 資訊
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="name">User name</param>
        /// <param name="phone">User phone</param>
        /// <remarks>
        /// </remarks>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, string name, int phone)
        {
            try
            {
                await _UserServices.UpadteUser(new User { Id = id , Name = name, Phone = phone});
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_UserServices.UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Users
        /// <summary>
        /// 新增 User 資訊
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="phone">User phone</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get / Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "phone": 091234567
        ///     }
        /// </remarks>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserResponse>> PostUser(string name, int phone)
        {
            var responseuser = await _UserServices.AddUser(new User{ Name = name , Phone = phone });
            return CreatedAtAction("GetUser", new { id = responseuser.Id }, responseuser);
        }

        // DELETE: api/Users/5
        /// <summary>
        /// 刪除 User 資訊
        /// </summary>
        /// <param name="id">User id</param>
        /// <remarks>
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var responseuser = await _UserServices.DeleteUser(id);
            if(!responseuser) return NotFound();
            else return NoContent();
        }


    }
}
