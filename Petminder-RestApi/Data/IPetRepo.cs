using System.Collections.Generic;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public interface IPetRepo
    {
        bool SaveChanges();

        IEnumerable<Pets> GetAllUserPets();
        Pets GetPetById(int Id);
        void CreatePet(Pets Pet);
    }
}