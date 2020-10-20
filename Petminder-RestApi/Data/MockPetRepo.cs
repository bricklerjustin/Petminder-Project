using System.Collections.Generic;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public class MockPetRepo : IPetRepo
    {
        public Pets GetPetById(int Id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Pets> GetAllUserPets()
        {
            var pets = new List<Pets>
            {
             new Pets{PetId=0},
                //new Pet{id=2,name="Test1",age=2,weight=80,gender="male",type="dog",breed=""},
                //new Pet{id=3,name="Test2",age=3,weight=35,gender="male",type="dog",breed=""},
                //new Pet{id=4,name="Test3",age=4,weight=10,gender="male",type="dog",breed=""} 
            };

            return pets;           
        }
    }
}