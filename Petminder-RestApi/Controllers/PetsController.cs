using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Petminder_RestApi.Data;
using Petminder_RestApi.Models;
using Petminder_RestApi.Dtos;

namespace Petminder_RestApi.Controllers
{
    //api/pets
    [Route("api/pets")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetRepo _repository;
        private readonly IMapper _mapper;

        public PetsController(IPetRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/pets
        [HttpGet]
        public ActionResult <IEnumerable<PetReadDto>> GetAllUserPets()
        {
            var PetItems = _repository.GetAllUserPets();

            return Ok(_mapper.Map<IEnumerable<PetReadDto>>(PetItems));
        }

        //GET api/pets/{id}
        [HttpGet("{id}")]
        public ActionResult <PetReadDto> GetPetById(int id)
        {
            var Pet = _repository.GetPetById(id);
            if (Pet != null)
            {
                return Ok(_mapper.Map<PetReadDto>(Pet));
            }

            return NotFound();
            
        }
    }
}