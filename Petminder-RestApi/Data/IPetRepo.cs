using System.Collections.Generic;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public interface IPetRepo
    {
        IEnumerable<Pets> GetAllUserPets();
        Pets GetPetById(int Id);
    }
}