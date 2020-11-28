using System;
using System.Collections.Generic;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public interface IPetRepo
    {
        bool SaveChanges();
        // bool ValidateAccountKey(Guid AccountId);

        IEnumerable<Pets> GetAllUserPets(Guid AccountId);
        Pets GetPetById(Guid id, Guid AccountId);
        void CreatePet(Pets Pet);
        void UpdatePet(Pets Pet);
        void DeletePet(Pets Pet);
    }
}