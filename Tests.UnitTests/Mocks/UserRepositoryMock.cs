using Data.Domain.Entity;
using Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.UnitTests.Mocks
{
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
