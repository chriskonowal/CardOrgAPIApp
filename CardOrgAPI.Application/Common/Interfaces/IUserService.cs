using CardOrgAPI.Application.Authentication;
using CardOrgAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Interfaces
{
    public interface IUserService
    {
        bool IsValidUserInformation(User model);

        User GetUserDetails();
    }
}
