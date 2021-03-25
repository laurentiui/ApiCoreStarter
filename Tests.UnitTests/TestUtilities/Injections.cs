using Data.Repository.Interfaces;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.UnitTests.Mocks;

namespace Tests.UnitTests.TestUtilities
{
    public class Injections
    {
        public IUserRepository userRepository;

        public IUserService userService;

        public Injections()
        {
            userRepository = new UserRepositoryMock();

            userService = new UserService(userRepository);
        }
    }
}
