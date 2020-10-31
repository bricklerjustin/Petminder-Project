using System;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public interface IAuthenticateRepo
    {
        bool ValidateToken(string Token, Guid Account);

        Users GetUserByUserAuth(string Username, string Password);
        Users GetUserByToken(string Token);
        Accounts GetAccountByToken(string Token);
    }
}