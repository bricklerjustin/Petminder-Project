using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Petminder_RestApi.Data;
using Petminder_RestApi.Dtos;
using Petminder_RestApi.Models;
using System;
using System.Text;
using Microsoft.AspNetCore.JsonPatch;

namespace Petminder_RestApi.Controllers
{
     //api/pets
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepo _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticateRepo _validate;
        private readonly IFileDataRepo _data;

        public FileController(IFileRepo repository, IMapper mapper, IAuthenticateRepo validate, IFileDataRepo data)
        {
            _repository = repository;
            _mapper = mapper;
            _validate = validate;
            _data = data;
        }

        //GET api/files
        [HttpGet]
        public ActionResult <IEnumerable<FileReadDto>> GetAllUserFileInfo()
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

            var FileItems = _repository.GetAllUserFiles(accountModel.Id);

            return Ok(_mapper.Map<IEnumerable<FileReadDto>>(FileItems));
        }

        //Post api/files
        [HttpPost]
        public ActionResult <FileReadDto> CreateFile(FileCreateDto fileCreateDto)
        {   
            var fileModel = _mapper.Map<Files>(fileCreateDto);
            var fileDataModel = _mapper.Map<Filedata>(fileCreateDto);

            if(_validate.GetAccountById(fileModel.AccountId) == null)
            {
                ModelState.AddModelError("accountId", $"The account with key: {fileModel.AccountId}, does not exist");
                return ValidationProblem();
            }

            _repository.CreateFile(fileModel);
            _repository.SaveChanges();
            _data.CreateFileData(fileDataModel);
            _repository.SaveChanges();

            var fileReadDto = _mapper.Map<FileReadDto>(fileModel);

            return CreatedAtAction(nameof(CreateFile), new {id = fileReadDto.Id}, fileReadDto);
        }

        [HttpGet("data/{id}")]
        public ActionResult <string> GetFileDataById(Guid id)
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

            if(_repository.GetFileById(id, accountModel.Id) == null)
            {
                return Unauthorized();
            }

            var filedata = _data.GetFileDataById(id);

            return Ok(filedata.Data);
        }

        [HttpGet("{id}")]
        public ActionResult<FileReadDto> GetFileById(Guid id)
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

            var fileModel = _repository.GetFileById(id, accountModel.Id);

            if (fileModel == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<FileReadDto>(fileModel));
        }

        //Put api/files
        [HttpPut("{id}")]
        public ActionResult UpdateFile(Guid id, FileUpdateDto fileUpdateDto)
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

            var fileModelFromRepo = _repository.GetFileById(id, accountModel.Id);
            if(fileModelFromRepo == null)
            {
                return NotFound();
            }
            
            _mapper.Map(fileUpdateDto, fileModelFromRepo);
            
            _repository.UpdateFile(fileModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //Delete api/files
        [HttpDelete("{id}")]
        public ActionResult DeleteFile(Guid id)
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

            var fileModelFromRepo = _repository.GetFileById(id, accountModel.Id);
            if(fileModelFromRepo == null)
            {
                return NotFound();
            }
            
            _repository.DeleteFile(fileModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/pets/{id}
        // [HttpPatch("{id}")]
        // public ActionResult PartialPetUpdate(Guid id, JsonPatchDocument<FileUpdateDto> patchDoc)
        // {
        //     if (!Request.Headers.ContainsKey("token"))
        //     {
        //         return Unauthorized();
        //     }

        //     var auth = Request.Headers["token"];
        //     var accountModel = _validate.GetAccountByToken(auth);

        //     if (accountModel == null)
        //     { 
        //         return Unauthorized();
        //     }

        //     var fileModelFromRepo = _repository.GetFileById(id, accountModel.Id);
        //     if(fileModelFromRepo == null)
        //     {
        //         return NotFound();
        //     }

        //     var petToPatch = _mapper.Map<PetUpdateDto>(fileModelFromRepo);
        //     patchDoc.ApplyTo(petToPatch, ModelState);
        //     if(!TryValidateModel(petToPatch))
        //     {
        //         return ValidationProblem(ModelState);
        //     }

        //     _mapper.Map(petToPatch, fileModelFromRepo);

        //     _repository.UpdatePet(fileModelFromRepo);

        //     _repository.SaveChanges();

        //     return NoContent();
        // }
    }
}