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
    //api/favorite
    [Route("api/favorite")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteRepo _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticateRepo _validate;

        public FavoriteController(IFavoriteRepo repository, IMapper mapper, IAuthenticateRepo validate)
        {
            _repository = repository;
            _mapper = mapper;
            _validate = validate;
        }

        //GET api/favorite
        [HttpGet]
        public ActionResult <IEnumerable<FavoriteReadDto>> GetAllUserFavorites()
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

            var FavoriteItems = _repository.GetAllUserFavorites(accountModel.Id);

            return Ok(_mapper.Map<IEnumerable<FavoriteReadDto>>(FavoriteItems));
        }

        //GET api/favorite
        [HttpGet("{Id}")]
        public ActionResult <IEnumerable<ExerciseReadDto>> GetFavoriteById(Guid Id)
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

            var Exercise = _repository.GetFavoiteById(Id, accountModel.Id);
            if (Exercise != null)
            {
                return Ok(_mapper.Map<ExerciseReadDto>(Exercise));
            }

            return NotFound();           
        }

        //POST api/favorite
        [HttpPost]
        public ActionResult <ExerciseReadDto> CreateFavorite(ExerciseUpdateCreateBaseDto exerciseUpdateCreate)
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

        //PUT api/favorite/{id}
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

        //DELETE api/favorite/{id}
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