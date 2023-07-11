using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repoitory;
using WebApi.Services;
using Xunit;

namespace WebApi.Tests
{
    public class UsersControllerTests
    {
        public class StubContext<T>
        {
            public IncrudContext GetUserContext()
            {
                var options = new DbContextOptionsBuilder<IncrudContext>()
                               .UseInMemoryDatabase(Guid.NewGuid().ToString())
                               .Options;

                var context = new IncrudContext(options);

                context.Users.Add(new User { Id = 100, Name = "Kevin" , Phone = 123456789 });
                context.Users.Add(new User { Id = 101, Name = "ABC", Phone = 333333333 });
                context.SaveChanges();

                return context;
            }
        }

        //告訴編譯器要執行的測試方法
        [Fact]
        public async void GetUsers()
        {
            // Arrange
            var stubCt = new StubContext<IncrudContext>().GetUserContext();
            var userServices = new UserServices(stubCt);
            //Act
            var Actual = userServices.GetAllUser().Result;
            //Assert
            Assert.NotNull(Actual);
        }

        [Fact]
        public async void GetUser()
        {
            // Arrange
            var stubCt = new StubContext<IncrudContext>().GetUserContext();
            var userServices = new UserServices(stubCt);

            int id = 100;
            //Act
            var Actual = userServices.GetUser(id).Result;
            //Assert
            Assert.Equal(id,Actual.Id);
        }

        [Fact]
        public async void AddUser()
        {
            // Arrange
            var stubCt = new StubContext<IncrudContext>().GetUserContext();
            var userServices = new UserServices(stubCt);

            User user = new User { Id = 102, Name = "test 2", Phone = 1234567123 };
            //Act
            var Actual = userServices.AddUser(user).Result;
            //Assert
            Assert.Equal(user.Id, Actual.Id);
        }

        [Fact]
        public async void DeleteUser()
        {
            // Arrange
            var stubCt = new StubContext<IncrudContext>().GetUserContext();
            var userServices = new UserServices(stubCt);

            int id = 100;
            //Act
            var Actual = userServices.DeleteUser(id).Result;
            //Assert
            Assert.False(!Actual);
        }
    }
}
