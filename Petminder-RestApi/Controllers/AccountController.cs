using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Petminder_RestApi.Data;
using Petminder_RestApi.Dtos;
using Petminder_RestApi.Models;
using Petminder_RestApi.Helpers;
using System;
using System.Net;

namespace Petminder_RestApi.Controllers
{
    //api/accounts
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepo _repository;
        private readonly IMapper _mapper;

        public AccountsController(IAccountRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/accounts
        [HttpPost]
        public ActionResult <AccountReadDto> CreateAccount([FromBody] AccountCreateDto accountCreateDto)
        {
            var accountModel = _mapper.Map<Accounts>(accountCreateDto);

            accountModel.ApiKey = KeyGenerator.GetUniqueKey(128);
            accountModel.CreateDate = DateTime.Now;

            while (!_repository.ValidateNewKey(accountModel.ApiKey))
            {
                accountModel.ApiKey = KeyGenerator.GetUniqueKey(128);
            }

            _repository.CreateAccount(accountModel);
            _repository.SaveChanges();

            var accountReadDto = _mapper.Map<AccountReadDto>(accountModel);

            return CreatedAtAction(nameof(CreateAccount), accountReadDto);
        }

        // [HttpDelete]
        // public ActionResult DeleteAccount(Guid id)
        // {
        //     if (!Request.Headers.ContainsKey("token"))
        //     {
        //         return Unauthorized();
        //     }

        //     var auth = Request.Headers["token"];

        //     if (auth == null)
        //     {
        //         return Unauthorized();
        //     }

        //     var account = _repository.GetAccountById(id, auth.ToString());

        //     return Ok();
        // }
    }
}