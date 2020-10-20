using System.Collections.Generic;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    interface IUserRepo
    {
        User GetUserByAuth();
        IEnumerable<User> GetUsersOnAccount();
    }
}