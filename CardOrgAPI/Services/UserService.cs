using CardOrgAPI.Entities;
using CardOrgAPI.Interfaces.Servics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CardOrgAPI.Services
{
    public class UserService : IUserService
    {
        public bool IsValidUserInformation(User model)
        {
            if (model.UserName.Equals("test") && model.Password.Equals("test")) return true;
            else return false;
        }

        public User GetUserDetails()
        {
            return new User { UserName = "Jay", Password = "123456" };
        }

    }
}
