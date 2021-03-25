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
        [Test]
        public async Task Test_RegisterUser_EmailIsNew_ReceiveConfirmToken()
        {
            var user = await _userService.RegisterAsync("username", "non-existing", "testpass");
            Assert.NotNull(user);
            Assert.NotNull(user.ConfirmToken);
            Assert.IsFalse(user.IsAllowed);
        }

        [Test]
        public async Task Test_ConfirmUser_ConfirmTokenWrong_ThrowsError()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _userService.ConfirmUserAsync("non-existing"));
            Assert.AreEqual("confirmation token failed", ex.Message);
        }

        [Test]
        public async Task Test_ConfirmUser_ConfirmTokenCorect_UserIsRegistered()
        {
            var user = await _userService.ConfirmUserAsync("a-sample-token");
            Assert.NotNull(user);
            Assert.IsNull(user.ConfirmToken);
            Assert.IsTrue(user.IsAllowed);
        }
    }

    internal class UserRepositoryMock : IUserRepository
    {
        public User GetByConfirmToken(string confirmToken)
        {
            if (confirmToken == "non-existing")
                return null;

            return new User()
            {
                ConfirmToken = confirmToken,
                IsAllowed = false
            };
        }
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
        public async Task<User> Update(User entity)
        {
            return entity;
        }

        public async Task<User> GetById(int playerId)
        {
            return null;
        }
        public async Task<IList<User>> ListAll()
        {
            return new List<User>();
        }
        public async Task Delete(User toRemove)
        {
        }

    }
}
