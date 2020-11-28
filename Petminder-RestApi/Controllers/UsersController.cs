using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Petminder_RestApi.Data;
using Petminder_RestApi.Dtos;
using Petminder_RestApi.Models;
using Petminder_RestApi.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using System.Net.Http.Headers;

namespace Petminder_RestApi.Controllers
{
    //api/authenticate
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticateRepo _validate;

        public UserController(IUserRepo repository, IMapper mapper, IAuthenticateRepo validate)
        {
            _repository = repository;
            _mapper = mapper;
            _validate = validate;
        }

        //POST api/users
        [HttpPost]
        public ActionResult <UserReadDto> CreateUser(UserCreateDto userCreateDto)
        {
            var auth = "";

            if (Request.Headers.ContainsKey("token"))
            {
                auth = Request.Headers["token"];
            }

            var userModel = _mapper.Map<Users>(userCreateDto);

            if (!_repository.ValidateAccountKey(userModel.AccountId))
            {
                ModelState.AddModelError("accountId", $"The account with key: {userModel.AccountId}, does not exist");
                return ValidationProblem();
            }

            if (!_validate.ValidateToken(auth, userModel.AccountId))
            {
                return Unauthorized();
            }

            userModel.ApiKey = auth;


            if (_repository.ValidateUsernameUnique(userModel.Username))
            {
                _repository.CreateUser(userModel);
                _repository.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("username", $@"The username: '{userModel.Username}' is already taken");
                return ValidationProblem();
            }

            var userReadDto = _mapper.Map<UserReadDto>(userModel);

            return CreatedAtAction(nameof(CreateUser), new {id = userReadDto.Id}, userReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(Guid id, UserUpdateDto userUpdateDto)
        {
            var auth = "";

            if (Request.Headers.ContainsKey("token"))
            {
                auth = Request.Headers["token"];
            }

            var userModel = _repository.GetUserById(id);
            if(userModel == null)
            {
                return NotFound();
            }

            if (!_validate.ValidateToken(auth, userModel.AccountId))
            {
                return Unauthorized();
            }
            
            _mapper.Map(userUpdateDto, userModel);
            
            _repository.UpdateUser(userModel);

            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/users/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialUserUpdate(Guid id, JsonPatchDocument<UserUpdateDto> patchDoc)
        {
            if (!Request.Headers.ContainsKey("token"))
            {
                return Unauthorized();
            }

            var auth = Request.Headers["token"];

            var userModelFromRepo = _repository.GetUserById(id);
            if(userModelFromRepo == null)
            {
                return NotFound();
            }

            if (!_validate.ValidateToken(auth, userModelFromRepo.AccountId))
            {
                return Unauthorized();
            }

            var userToPatch = _mapper.Map<UserUpdateDto>(userModelFromRepo);
            patchDoc.ApplyTo(userToPatch, ModelState);
            if(!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(userToPatch, userModelFromRepo);

            _repository.UpdateUser(userModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/pets/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(Guid id)
        {
            if (!Request.Headers.ContainsKey("token"))
            {
                return Unauthorized();
            }

            var auth = Request.Headers["token"];

            var userModelFromRepo = _repository.GetUserById(id);
            if(userModelFromRepo == null)
            {
                return NotFound();
            }

            if (!_validate.ValidateToken(auth, userModelFromRepo.AccountId))
            {
                return Unauthorized();
            }
            
            _repository.DeleteUser(userModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}