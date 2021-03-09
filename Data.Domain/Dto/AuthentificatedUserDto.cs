using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.Dto
{
    public class AuthentificatedUserDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
