using System;
using System.Linq;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public class SqlAccountRepo : IAccountRepo
    {
        private readonly PetminderContext _context;

        public SqlAccountRepo(PetminderContext context)
        {
            _context = context;
        }

        public void CreateAccount(Accounts Account)
        {
            if (Account == null)
            {
                throw new ArgumentNullException(nameof(Account));
            }
            
            _context.Accounts.Add(Account);
        }

        public void DeleteAccount(Accounts Account)
        {
            if (Account == null)
            {
                throw new ArgumentNullException(nameof(Account));
            }
            
            _context.Accounts.Remove(Account);
        }

        public Accounts GetAccountById(Guid id, string auth)
        {
            return _context.Accounts.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
           return (_context.SaveChanges() > 0);
        }

        public bool ValidateNewKey(string Key)
        {
            var account = _context.Accounts.FirstOrDefault(p => p.ApiKey == Key);

            if (account != null)
            {
                return false;
            }

            return true;
        }
    }
}