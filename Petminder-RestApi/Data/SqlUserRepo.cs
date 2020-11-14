using System;
using System.Linq;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public class SqlUserRepo : IUserRepo
    {
        private readonly PetminderContext _context;

        public SqlUserRepo(PetminderContext context)
        {
            _context = context;
        }

        public void CreateUser(Users User)
        {
            if (User == null)
            {
                throw new ArgumentNullException(nameof(User));
            }
            
            _context.Users.Add(User);
        }

        public void DeleteUser(Users User)
        {
            if (User == null)
            {
                throw new ArgumentNullException(nameof(User));
            }

            _context.Users.Remove(User);
        }

        public Users GetUserById(Guid Id)
        {
            var dbpetreturn = _context.Users.Where((p) => p.Id == Id);

            return dbpetreturn.SingleOrDefault();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateUser(Users User)
        {
            //Nothing
        }

        public bool ValidateAccountKey(Guid AccountId)
        {
            var account = _context.Accounts.FirstOrDefault((p) => p.Id == AccountId);

            if (account == null)
            {
                return false;
            }

            return true;
        }

        public bool ValidateUsernameUnique(string Username)
        {
            var user = _context.Users.FirstOrDefault(p => p.Username == Username);

            if (user == null)
            {
                return true;
            }

            return false;
        }
    }
}