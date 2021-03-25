using Data.Domain.Entity;
using Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.UnitTests.Mocks
{
    public class UserRepositoryMock : BaseRepositoryMock<User>, IUserRepository
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

    }
}
