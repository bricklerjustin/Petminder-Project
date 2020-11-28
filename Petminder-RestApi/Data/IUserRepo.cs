using System;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public interface IUserRepo
    {
        bool SaveChanges();
        bool ValidateAccountKey(Guid AccountId);
        bool ValidateUsernameUnique(string Username);

        void CreateUser(Users User);
        Users GetUserById(Guid Id);
        void UpdateUser(Users User);
        void DeleteUser(Users User);
    }
}