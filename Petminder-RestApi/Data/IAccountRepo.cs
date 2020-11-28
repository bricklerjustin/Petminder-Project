using System;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public interface IAccountRepo 
    {
        bool SaveChanges();
        bool ValidateNewKey(string Key);

        Accounts GetAccountById(Guid id, string auth);
        void CreateAccount(Accounts Account);
        void DeleteAccount(Accounts Account);

    }
}