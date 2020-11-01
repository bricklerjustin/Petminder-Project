using System;
using System.Linq;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public class SqlAuthenticateRepo : IAuthenticateRepo
    {
        private readonly PetminderContext _context;

        public SqlAuthenticateRepo(PetminderContext context)
        {
            _context = context;
        }

        public Accounts GetAccountById(Guid AccountId)
        {
           var account = _context.Accounts.FirstOrDefault(p => p.Id == AccountId);

           return account;
        }

        public Accounts GetAccountByToken(string Token)
        {
            var Account = _context.Accounts.FirstOrDefault(p => p.ApiKey == Token);

            return Account;
        }

        public Users GetUserByToken(string Token)
        {
            var user = _context.Users.FirstOrDefault(p => p.ApiKey == Token);

            return user;
        }

        public Users GetUserByUserAuth(string Username, string Password)
        {
            var user = _context.Users.Where((p) => p.Username == Username && p.PasswordHash == Password);

            return user.SingleOrDefault();
        }

        public bool ValidateToken(string Token, Guid AccountId)
        {
            var account = _context.Accounts.FirstOrDefault(p => p.ApiKey == Token && p.Id == AccountId);

            if (account != null)
            {
                return true;
            }

            return false;
        }
    }
}