using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Petminder_RestApi.Data;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Controllers
{
    //api/pets
    [Route("api/pets")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetRepo _repository;

        public PetsController(IPetRepo repository)
        {
            _repository = repository;
        }

        //GET api/pets
        [HttpGet]
        public ActionResult <IEnumerable<Pets>> GetAllUserPets()
        {
            var PetItems = _repository.GetAllUserPets();

            return Ok(PetItems);
        }

        //GET api/pets/{id}
        [HttpGet("{id}")]
        public ActionResult <Pets> GetPetById(int id)
        {
            var Pet = _repository.GetPetById(id);

            return Ok(Pet);
        }
    }
}