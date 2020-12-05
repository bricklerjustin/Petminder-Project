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
        public ActionResult <FavoriteReadDto> GetFavoriteById(Guid Id)
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

            var Favorite = _repository.GetFavoiteById(Id, accountModel.Id);
            if (Favorite != null)
            {
                return Ok(_mapper.Map<FavoriteReadDto>(Favorite));
            }

            return NotFound();           
        }

        //POST api/favorite
        [HttpPost]
        public ActionResult <FavoriteReadDto> CreateFavorite(FavoriteUpdateCreateBaseDto favoriteUpdateCreate)
        {
            var favoriteModel = _mapper.Map<Favorites>(favoriteUpdateCreate);

            if(_validate.GetAccountById(favoriteModel.AccountId) == null)
            {
                ModelState.AddModelError("accountId", $"The account with key: {favoriteModel.AccountId}, does not exist");
                return ValidationProblem();
            }

            _repository.CreateFavorite(favoriteModel);
            _repository.SaveChanges();

            var favoriteReadDto = _mapper.Map<FavoriteReadDto>(favoriteModel);

            return CreatedAtAction(nameof(favoriteModel), new {id = favoriteReadDto.Id}, favoriteReadDto);
        }

        //PUT api/favorite/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateFavorite(Guid id, FavoriteUpdateCreateBaseDto favoriteUpdateDto)
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

            var favoriteModelFromRepo = _repository.GetFavoiteById(id, accountModel.Id);
            if(favoriteModelFromRepo == null)
            {
                return NotFound();
            }
            
            _mapper.Map(favoriteUpdateDto, favoriteModelFromRepo);
            
            _repository.UpdateFavorite(favoriteModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/favorite/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteFavorite(Guid id)
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

            var favoriteModelFromRepo = _repository.GetFavoiteById(id, accountModel.Id);
            if(favoriteModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteFavorite(favoriteModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}