using System;
using System.Collections.Generic;
using System.Linq;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public class SqlPetRepo : IPetRepo
    {
        private readonly PetminderContext _context;

        public SqlPetRepo(PetminderContext context)
        {
            _context = context;
        }

        public void CreatePet(Pets Pet)
        {
            if (Pet == null)
            {
                throw new ArgumentNullException(nameof(Pet));
            }
            
            _context.Pets.Add(Pet);
        }

        public IEnumerable<Pets> GetAllUserPets(Guid AccountId)
        {
            return _context.Pets.Where(p => p.AccountId == AccountId);
        }

        public Pets GetPetById(Guid Id, Guid AccountId)
        {
            var dbpetreturn = _context.Pets.Where((p) => p.Id == Id && p.AccountId == AccountId);

            return dbpetreturn.SingleOrDefault();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdatePet(Pets Pet)
        {
            //NOT IMPLMENTED Done by other methods
        }

        public void DeletePet(Pets Pet)
        {
            if (Pet == null)
            {
                throw new ArgumentNullException(nameof(Pet));
            }

            _context.Pets.Remove(Pet);
        }
    }
}
