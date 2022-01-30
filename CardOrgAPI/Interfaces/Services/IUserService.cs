using CardOrgAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Interfaces.Servics
{
    public interface IUserService
    {
        bool IsValidUserInformation(User model);

        User GetUserDetails();
    }
}
