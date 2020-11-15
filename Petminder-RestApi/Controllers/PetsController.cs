using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Petminder_RestApi.Data;
using Petminder_RestApi.Models;
using Petminder_RestApi.Dtos;
using System;
using Microsoft.AspNetCore.JsonPatch;

namespace Petminder_RestApi.Controllers
{
    //api/pets
    [Route("api/pets")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetRepo _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticateRepo _validate;

        public PetsController(IPetRepo repository, IMapper mapper, IAuthenticateRepo validate)
        {
            _repository = repository;
            _mapper = mapper;
            _validate = validate;
        }

        //GET api/pets
        [HttpGet]
        public ActionResult <IEnumerable<PetReadDto>> GetAllUserPets()
        {
            if (!Request.Headers.ContainsKey("token"))
            {
                return Unauthorized();
            }

            var auth = Request.Headers["token"];
            var accountModel = _validate.GetAccountByToken(auth);

            if (accountModel == null)
            { 
                return Unauthorized();
            }

            var PetItems = _repository.GetAllUserPets(accountModel.Id);

            return Ok(_mapper.Map<IEnumerable<PetReadDto>>(PetItems));
        }

        //GET api/pets/{id}
        [HttpGet("{id}", Name="GetPetById")]
        public ActionResult <PetReadDto> GetPetById(Guid id)
        {
            if (!Request.Headers.ContainsKey("token"))
            {
                return Unauthorized();
            }

            var auth = Request.Headers["token"];
            var accountModel = _validate.GetAccountByToken(auth);

            if (accountModel == null)
            { 
                return Unauthorized();
            }

            var Pet = _repository.GetPetById(id, accountModel.Id);
            if (Pet != null)
            {
                return Ok(_mapper.Map<PetReadDto>(Pet));
            }

            return NotFound();           
        }

        //POST api/pets
        [HttpPost]
        public ActionResult <PetReadDto> CreatePet(PetCreateDto petCreateDto)
        {
            var petModel = _mapper.Map<Pets>(petCreateDto);

            if(_validate.GetAccountById(petModel.AccountId) == null)
            {
                ModelState.AddModelError("accountId", $"The account with key: {petModel.AccountId}, does not exist");
                return ValidationProblem();
            }

            _repository.CreatePet(petModel);
            _repository.SaveChanges();

            var petReadDto = _mapper.Map<PetReadDto>(petModel);

            return CreatedAtRoute(nameof(GetPetById), new {id = petReadDto.Id}, petReadDto);
        }

        //PUT api/pets/{id}
        [HttpPut("{id}")]
        public ActionResult UpdatePet(Guid id, PetUpdateDto petUpdateDto)
        {
            if (!Request.Headers.ContainsKey("token"))
            {
                return Unauthorized();
            }

            var auth = Request.Headers["token"];
            var accountModel = _validate.GetAccountByToken(auth);

            if (accountModel == null)
            { 
                return Unauthorized();
            }

            var petModelFromRepo = _repository.GetPetById(id, accountModel.Id);
            if(petModelFromRepo == null)
            {
                return NotFound();
            }
            
            _mapper.Map(petUpdateDto, petModelFromRepo);
            
            _repository.UpdatePet(petModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/pets/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialPetUpdate(Guid id, JsonPatchDocument<PetUpdateDto> patchDoc)
        {
            if (!Request.Headers.ContainsKey("token"))
            {
                return Unauthorized();
            }

            var auth = Request.Headers["token"];
            var accountModel = _validate.GetAccountByToken(auth);

            if (accountModel == null)
            { 
                return Unauthorized();
            }

            var petModelFromRepo = _repository.GetPetById(id, accountModel.Id);
            if(petModelFromRepo == null)
            {
                return NotFound();
            }

            var petToPatch = _mapper.Map<PetUpdateDto>(petModelFromRepo);
            patchDoc.ApplyTo(petToPatch, ModelState);
            if(!TryValidateModel(petToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(petToPatch, petModelFromRepo);

            _repository.UpdatePet(petModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/pets/{id}
        [HttpDelete("{id}")]
        public ActionResult DeletePet(Guid id)
        {
            if (!Request.Headers.ContainsKey("token"))
            {
                return Unauthorized();
            }

            var auth = Request.Headers["token"];
            var accountModel = _validate.GetAccountByToken(auth);

            if (accountModel == null)
            { 
                return Unauthorized();
            }

            var petModelFromRepo = _repository.GetPetById(id, accountModel.Id);
            if(petModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeletePet(petModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}