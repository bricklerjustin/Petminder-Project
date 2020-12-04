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
    //api/exercise
    [Route("api/exercise")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseRepo _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticateRepo _validate;

        public ExerciseController(IExerciseRepo repository, IMapper mapper, IAuthenticateRepo validate)
        {
            _repository = repository;
            _mapper = mapper;
            _validate = validate;
        }

        //GET api/exercise
        [HttpGet("pet/{id}")]
        public ActionResult <IEnumerable<ExerciseReadDto>> GetAllPetExercises(Guid Id)
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

            var ExerciseItems = _repository.GetAllPetExercises(Id, accountModel.Id);

            return Ok(_mapper.Map<IEnumerable<ExerciseReadDto>>(ExerciseItems));
        }

        //GET api/exercise
        [HttpGet]
        public ActionResult <IEnumerable<ExerciseReadDto>> GetAllExercises()
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

            var Exercise = _repository.GetAllExercises(accountModel.Id);
            if (Exercise != null)
            {
                return Ok(_mapper.Map<ExerciseReadDto>(Exercise));
            }

            return NotFound();           
        }

        //POST api/exercise
        [HttpPost]
        public ActionResult <ExerciseReadDto> CreateExercise(ExerciseUpdateCreateBaseDto exerciseUpdateCreate)
        {
            var exerciseModel = _mapper.Map<Exercises>(exerciseUpdateCreate);

            if(_validate.GetAccountById(exerciseModel.AccountId) == null)
            {
                ModelState.AddModelError("accountId", $"The account with key: {exerciseModel.AccountId}, does not exist");
                return ValidationProblem();
            }

            _repository.CreateExercise(exerciseModel);
            _repository.SaveChanges();

            var exerciseReadDto = _mapper.Map<ExerciseReadDto>(exerciseModel);

            return CreatedAtAction(nameof(CreateExercise), new {id = exerciseReadDto.Id}, exerciseReadDto);
        }

        //PUT api/exercise/{id}
        [HttpPut("{id}")]
        public ActionResult UpdatePet(Guid id, ExerciseUpdateCreateBaseDto exerciseUpdateDto)
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

            var exerciseModelFromRepo = _repository.GetExerciseById(id, accountModel.Id);
            if(exerciseModelFromRepo == null)
            {
                return NotFound();
            }
            
            _mapper.Map(exerciseUpdateDto, exerciseModelFromRepo);
            
            _repository.UpdateExercise(exerciseModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/exercise/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteExercise(Guid id)
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

            var exerciseModelFromRepo = _repository.GetExerciseById(id, accountModel.Id);
            if(exerciseModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteExercise(exerciseModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}