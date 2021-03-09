using Data.Domain.Entity;
using Data.Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null)
                throw new ArgumentException("user not not found");

            string encryptedPassword = Utilities.Crypt.CreateMD5(password);
            if (encryptedPassword != user.Password)
                throw new ArgumentException("pass not not found");

            if (!user.IsAllowed)
                throw new ArgumentException("user is blocked");

            return user;
        }
        public async Task<User> RegisterAsync(string username, string email, string password)
        {
            var user = _userRepository.GetByEmail(email);
            if (user != null)
                throw new ArgumentException("email already exists");

            var newEntity = new User()
            {
                Username = username,
                Email = email,
                Password = password,
                IsAllowed = false
            };

            newEntity = await _userRepository.Insert(newEntity);

            return user;
        }
    }
}
