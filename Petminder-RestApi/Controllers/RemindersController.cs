using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Petminder_RestApi.Data;
using Petminder_RestApi.Dtos;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Controllers
{
    //api/reminders
    [Route("api/reminders")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderRepo _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticateRepo _validate;

        public ReminderController(IReminderRepo repository, IMapper mapper, IAuthenticateRepo validate)
        {
            _repository = repository;
            _mapper = mapper;
            _validate = validate;
        }

        //GET api/reminders
        [HttpGet]
        public ActionResult <IEnumerable<ReminderReadDto>> GetAllUserReminders()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }

            var auth = Request.Headers["Authorization"];
            var accountModel = _validate.GetAccountByToken(auth);

            if (accountModel == null)
            { 
                return Unauthorized();
            }

            var ReminderItems = _repository.GetAllUserReminders(accountModel.Id);

            return Ok(_mapper.Map<IEnumerable<ReminderReadDto>>(ReminderItems));
        }

        //GET api/reminders/{id}
        [HttpGet("{id}", Name="GetReminderById")]
        public ActionResult <ReminderReadDto> GetReminderById(Guid id)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }

            var auth = Request.Headers["Authorization"];
            var accountModel = _validate.GetAccountByToken(auth);

            if (accountModel == null)
            { 
                return Unauthorized();
            }

            var Reminder = _repository.GetReminderById(id, accountModel.Id);
            if (Reminder != null)
            {
                return Ok(_mapper.Map<ReminderReadDto>(Reminder));
            }

            return NotFound();           
        }

        //POST api/reminders
        [HttpPost]
        public ActionResult <ReminderReadDto> CreateReminder(RemidnerCreateDto reminderCreateDto)
        {
            var reminderModel = _mapper.Map<Reminders>(reminderCreateDto);

            if(_validate.GetAccountById(reminderModel.AccountId) == null)
            {
                ModelState.AddModelError("accountId", $"The account with key: {reminderModel.AccountId}, does not exist");
                return ValidationProblem();
            }

            _repository.CreateReminder(reminderModel);
            _repository.SaveChanges();

            var reminderReadDto = _mapper.Map<ReminderReadDto>(reminderModel);

            return CreatedAtRoute(nameof(GetReminderById), new {id = reminderReadDto.Id}, reminderReadDto);
        }

        //PUT api/reminders/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateReminder(Guid id, ReminderUpdateDto reminderUpdateDto)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }

            var auth = Request.Headers["Authorization"];
            var accountModel = _validate.GetAccountByToken(auth);

            if (accountModel == null)
            { 
                return Unauthorized();
            }

            var reminderModelFromRepo = _repository.GetReminderById(id, accountModel.Id);
            if(reminderModelFromRepo == null)
            {
                return NotFound();
            }
            
            _mapper.Map(reminderUpdateDto, reminderModelFromRepo);
            
            _repository.UpdateReminder(reminderModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/reminders/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialReminderUpdate(Guid id, JsonPatchDocument<ReminderUpdateDto> patchDoc)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }

            var auth = Request.Headers["Authorization"];
            var accountModel = _validate.GetAccountByToken(auth);

            if (accountModel == null)
            { 
                return Unauthorized();
            }

            var reminderModelFromRepo = _repository.GetReminderById(id, accountModel.Id);
            if(reminderModelFromRepo == null)
            {
                return NotFound();
            }

            var reminderToPatch = _mapper.Map<ReminderUpdateDto>(reminderModelFromRepo);
            patchDoc.ApplyTo(reminderToPatch, ModelState);
            if(!TryValidateModel(reminderToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(reminderToPatch, reminderModelFromRepo);

            _repository.UpdateReminder(reminderModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/reminders/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteReminder(Guid id)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized();
            }

            var auth = Request.Headers["Authorization"];
            var accountModel = _validate.GetAccountByToken(auth);

            if (accountModel == null)
            { 
                return Unauthorized();
            }

            var reminderModelFromRepo = _repository.GetReminderById(id, accountModel.Id);
            if(reminderModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteReminder(reminderModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}