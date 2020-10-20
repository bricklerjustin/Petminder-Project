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

        public IEnumerable<Pets> GetAllUserPets()
        {
            return _context.Pets.ToList();
        }

        public Pets GetPetById(int Id)
        {
            throw new System.NotImplementedException();
        }
    }
}
