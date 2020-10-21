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

        public IEnumerable<Pets> GetAllUserPets()
        {
            return _context.Pets.ToList();
        }

        public Pets GetPetById(int Id)
        {
            return _context.Pets.Where((p) => p.PetId == Id).Single();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
