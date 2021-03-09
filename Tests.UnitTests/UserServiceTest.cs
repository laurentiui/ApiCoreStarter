using Data.Domain.Entity;
using Data.Repository.Interfaces;
using NUnit.Framework;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.UnitTests
{
    public class UserServiceTest
    {
        private IUserService _userService;

        [SetUp]
        public void Setup()
        {
            var userRepository = new UserRepositoryMock();
            _userService = new UserService(userRepository);
        }

        [Test]
        public async Task Test_Login_EmailNotExists_ThrowException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _userService.LoginAsync("non-existing", "testpass"));
            Assert.AreEqual("user not not found", ex.Message);
        }
        [Test]
        public async Task Test_Login_IsPassWrong_ThrowException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _userService.LoginAsync("anything", "testpass-wrong"));
            Assert.AreEqual("pass not not found", ex.Message);
        }
        [Test]
        public async Task Test_Login_IsNotAllowed_ThrowException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _userService.LoginAsync("existing-not-allow", "testpass"));
            Assert.AreEqual("user is blocked", ex.Message);
        }
        [Test]
        public async Task Test_Login_Ok_GetUser()
        {
            var user = await _userService.LoginAsync("correct-user", "testpass");
            Assert.NotNull(user);
        }

        [Test]
        public async Task Test_RegisterUser_EmailExists_ThrowsError()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _userService.RegisterAsync("username", "existing", "testpass"));
            Assert.AreEqual("email already exists", ex.Message);
        }
    }

    internal class UserRepositoryMock : IUserRepository
    {
        public User GetByEmail(string email)
        {
            var user = new User()
            {
                Email = email,
                Password = Utilities.Crypt.CreateMD5("testpass"),
                IsAllowed = true
            };

            switch (email)
            {
                case "non-existing": return null;
                case "existing-not-allow":
                    user.IsAllowed = false;
                    return user;
                default: return user;
            }
            
        }
        public async Task<User> Insert(User newEntity)
        {
            return newEntity;
        }
    }
}
